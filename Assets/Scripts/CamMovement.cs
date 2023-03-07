using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;

    Vector3 position;
    public bool thirdperson = false;
    private Vector2 LookPos;
    private float Xrotation = 0f, Yrotation = 0f;

    [SerializeField]
    private float rotationSens = 5f;


    // Start is called before the first frame update
    void Start()
    {
        if (!thirdperson)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void LookAround()
    {

        if (thirdperson)
        {
            //cam.transform.LookAt(transform.position);
            Xrotation = -LookPos.x * rotationSens * Time.deltaTime;
            Yrotation = -LookPos.y * rotationSens * Time.deltaTime;

            cam.transform.RotateAround(transform.position, Vector3.up, Xrotation);

            cam.transform.Translate(new Vector3(0, Yrotation, 0));
            cam.transform.LookAt(transform.position);

        }
        else
        {
            Xrotation += -LookPos.y * rotationSens * Time.deltaTime;
            Xrotation = Mathf.Clamp(Xrotation, -80f, 80f);
            Yrotation += LookPos.x * rotationSens * Time.deltaTime;
            cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            cam.transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
            transform.rotation = Quaternion.Euler(0, Yrotation, 0);
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
            position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z - 10);
            GetComponent<MeshRenderer>().enabled = true;


        }
        cam.transform.position = position;
    }
}
