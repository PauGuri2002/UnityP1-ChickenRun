using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;
    private GameObject CamParent;


    Vector3 position;
    public bool thirdperson = false;
    private Vector2 LookPos;
    private float Xrotation = 0f, Zoom = 0f, Yrotation=0f;
    private Vector3 LastPosition = new Vector3(0,0,0);
    private Vector3 difference = new Vector3(0,0,0);
    [SerializeField]
    private float rotationSens = 5f, ZoomSens = 20f;
    [SerializeField]
    private chickenControl chickenControlscript;
    private Transform Hijo;



    // Start is called before the first frame update
    void Start()
    {
        LastPosition = transform.position;
        if (!thirdperson)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        Hijo = GetComponentInChildren<Transform>();
        CamParent = cam.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        Hijo.rotation = Quaternion.identity;


        difference = Hijo.position - LastPosition;


        //if (difference.magnitude > 0)
        //{
        //difference.y = Mathf.Clamp(difference.y, 0, 5);
        //difference.z = Mathf.Clamp(difference.z, -10, 10);
        LookAround();
        CamParent.transform.Translate(difference.x , difference.y, difference.z );


        //cam.transform.LookAt(transform.position);
        //}
        //cam.transform.position = transform.position;

        LastPosition = Hijo.position;

        
    }




    void LookAround()
    {

        if (thirdperson)
        {
            //cam.transform.LookAt(transform.position);
            Xrotation = LookPos.x * rotationSens * Time.deltaTime;
            Zoom = Zoom * ZoomSens * Time.deltaTime;
          
            //cam.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z - 10);
            if (Mathf.Abs(LookPos.x) > 0)
            {
                CamParent.transform.RotateAround(transform.position, Vector3.up, Xrotation);
                CamParent.transform.rotation = Quaternion.identity;

            }

            Camera.main.fieldOfView -= Zoom;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 30, 120);

            cam.transform.LookAt(transform.position);


        }
        else
        {
            Camera.main.fieldOfView = 90;

            Xrotation += -LookPos.y * rotationSens * Time.deltaTime;
            Xrotation = Mathf.Clamp(Xrotation, -80f, 80f);
            Yrotation += LookPos.x * rotationSens * Time.deltaTime;
            cam.transform.position = new Vector3(Hijo.position.x, Hijo.position.y + 1.5f, Hijo.position.z);
            cam.transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
            
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookPos = context.ReadValue<Vector2>();
    }

    public void OnToggleCamera()
    {
        thirdperson = thirdperson ? false : true;

        if (thirdperson == false)
        {
            position = new Vector3(Hijo.position.x, Hijo.position.y + 1.5f, Hijo.position.z);
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {

            position = new Vector3(Hijo.position.x, Hijo.position.y + 5, Hijo.position.z - 10);
            GetComponent<MeshRenderer>().enabled = true;


        }
        CamParent.transform.position = position;
    }
    public void OnZoom(InputAction.CallbackContext context)
    {
        Zoom = context.ReadValue<Vector2>().y;
        
    }
}
