using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;
    private GameObject CamParent;
    [SerializeField]
    private Transform camRotator;

    Vector3 position;
    private bool thirdperson = false;
    private Vector2 LookPos;
    private float Xrotation = 0f, Zoom = 0f, Yrotation=0f;
    private Vector3 LastPosition = new Vector3(0,0,0);
    private Vector3 difference = new Vector3(0,0,0);

    [SerializeField]
    private float rotationSens = 5f, ZoomSens = 20f, firstPersonHeight = 1f;
    [SerializeField]
    private GameObject playerRenderer;

    void Start()
    {
        LastPosition = camRotator.position;
        if (!thirdperson)
        {
            playerRenderer.SetActive(false);
        }
        CamParent = cam.transform.parent.gameObject;
    }

    void Update()
    {
        camRotator.rotation = Quaternion.identity;


        difference = camRotator.position - LastPosition;

        LookAround();
        CamParent.transform.Translate(difference.x , difference.y, difference.z );

        LastPosition = camRotator.position;
    }

    void LookAround()
    {

        if (thirdperson)
        {
            Xrotation = LookPos.x * rotationSens * Time.deltaTime;
            Zoom = Zoom * ZoomSens * Time.deltaTime;
          
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
            CamParent.transform.position = new Vector3(camRotator.position.x, camRotator.position.y + firstPersonHeight, camRotator.position.z);
            cam.transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookPos = context.ReadValue<Vector2>();
    }

    public void OnToggleCamera(InputAction.CallbackContext context)
    {
        if(context.performed || context.canceled) { return; }
        
        thirdperson = thirdperson ? false : true;

        if (thirdperson == false)
        {
            position = new Vector3(camRotator.position.x, camRotator.position.y + firstPersonHeight, camRotator.position.z);
            playerRenderer.SetActive(false);
        }
        else
        {

            position = new Vector3(camRotator.position.x, camRotator.position.y + 5, camRotator.position.z - 10);
            playerRenderer.SetActive(true);

        }
        CamParent.transform.position = position;
    }
    public void OnZoom(InputAction.CallbackContext context)
    {
        Zoom = context.ReadValue<Vector2>().y;
    }
}
