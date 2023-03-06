using UnityEngine;
using UnityEngine.InputSystem;

public class chikenControl : MonoBehaviour
{
    [SerializeField]
    private float Speed = 2f;
    [SerializeField]
    private float personalForce = 300f;
    [SerializeField]
    private float rotationSens = 5f;

    private Vector2 move = new Vector2(0, 0);

    Rigidbody gravity;
    
    bool isgrounded;

    float countJump = 0f;
    private float Xrotation = 0f, Yrotation= 0f;

    [SerializeField]
    public GameObject cam;

    Vector2 LookPos;
    
    // Start is called before the first frame update
    void Start()
    {
        gravity = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move.x * Speed * Time.deltaTime, 0, move.y * Speed * Time.deltaTime, Space.World);

        if (countJump == 2 && Input.GetKey(KeyCode.Space))
        {   
            gravity.velocity = 0.95f * gravity.velocity;

        }
        playerLook();
    }
    void playerLook()
    {
        Xrotation += -LookPos.y * rotationSens * Time.deltaTime;
        Xrotation = Mathf.Clamp(Xrotation, -80f, 80f);
        Yrotation += LookPos.x * rotationSens * Time.deltaTime;


        if (cam.GetComponent<CamMovement>().thirdperson)
        {

            cam.transform.LookAt(transform.position);
            cam.transform.RotateAround(transform.position, Vector3.up, LookPos.x);            
        }
        else
        {
            cam.transform.position=(transform.position);
            cam.transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
            transform.rotation = Quaternion.Euler(0, Yrotation, 0);
        }

    }

    void OnMove(InputValue WASD)
    {
        move = WASD.Get<Vector2>();
    }

    void OnLook(InputValue context)
    {
        LookPos = context.Get<Vector2>();
    }

    void OnJump()
    {
        if (isgrounded == true || countJump < 2)
        {
            gravity.AddForce(0, personalForce * gravity.mass, 0);
            countJump++;
            Debug.Log("jumpCount++");

            if (countJump == 2)
            {
                gravity.velocity = 0.95f * gravity.velocity;
            }
        }
    }
    void OnToggleCamera()
    {
        cam.GetComponent<CamMovement>().ToggleCam();
    }
    void OnSpeedUp()
    {
        Speed = 5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
            countJump = 0;
            Debug.Log("jumpCount Reset");
        }

        if (collision.gameObject.CompareTag("Killer"))
        {
            // handle player incapacitation
            Debug.Log("You have been hit, ouch");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = false;
        }
    }

}