using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class chickenControl : MonoBehaviour
{
    [SerializeField]
    private float Speed = 2f;
    private float speedLock = 2f;

    [SerializeField]
    private float rotationSens = 5f;

    [SerializeField]
    private float gravity = 9.81f;

    [SerializeField]
    private float highJump = 5f;

    float verticalMove;

    private Vector2 move = new Vector2(0, 0);

    CharacterController characterController;

    float countJump = 0f;

    private float Xrotation = 0f, Yrotation= 0f;

    [SerializeField]
    public GameObject cam;
    [SerializeField]


    Vector2 LookPos;
    
    // Start is called before the first frame update
    void Start()
    {

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        playerLook();
        

    }
    void playerLook()
    {


        if (cam.GetComponent<CamMovement>().thirdperson)
        {
            
            //cam.transform.LookAt(transform.position);
            Xrotation = LookPos.x * rotationSens;
            Yrotation = LookPos.y * rotationSens;
           

            //cam.transform.RotateAround(transform.position, Vector3.up, LookPos.x * rotationSens * Time.deltaTime);
            
            cam.transform.Translate(new Vector3((LookPos.x * Time.smoothDeltaTime) * -1,(LookPos.y*Time.smoothDeltaTime) * -1, 0));
            
            cam.transform.LookAt(transform.position);
            

        }
        else
        {
            Xrotation += -LookPos.y * rotationSens * Time.deltaTime;
            Xrotation = Mathf.Clamp(Xrotation, -80f, 80f);
            Yrotation += LookPos.x * rotationSens * Time.deltaTime;
            cam.transform.position=new Vector3 (transform.position.x, transform.position.y + 1.5f, transform.position.z);
            cam.transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
            transform.rotation = Quaternion.Euler(0, Yrotation, 0);
        }

    }

    void OnLook(InputValue context)
    {
        LookPos = context.Get<Vector2>();
    }

    void OnToggleCamera()
    {
        cam.GetComponent<CamMovement>().ToggleCam();
    }

    // MOVEMENT 
    void OnMove(InputValue WASD)
    {
        move = WASD.Get<Vector2>();
    }
    void OnJump()
    {
  
        if (characterController.isGrounded)
        {
            countJump = 0;
        }

        if (countJump >= 0 && countJump <= 1)
        {
            verticalMove = highJump;
            countJump++;
        }

        if(countJump == 1)
        {

        }
    }
    //void OnSpeedUp(CallbackContext isPressed)
    //{
    //    if (isPressed.performed)
    //    {
    //        Speed = 5f;
    //    }
    //    if (isPressed.canceled)
    //    {
    //        Speed = speedLock;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("collided with: " + collision.gameObject.tag);

    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isgrounded = true;
    //        countJump = 0;
    //        Debug.Log("jumpCount Reset");
    //    }

    //    if (collision.gameObject.CompareTag("Killer"))
    //    {
    //        // handle player incapacitation
    //        Debug.Log("You have been hit, ouch");
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isgrounded = false;
    //    }
    //}

    void Movement()
    {
        //camera forward and right vectors:
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        

        Vector3 horizontalMove = forward * move.y + right * move.x;

        verticalMove -= gravity * Time.deltaTime;

        Vector3 hvMove = new Vector3(horizontalMove.x, verticalMove, horizontalMove.z);
        //now we can apply the movement:
        characterController.Move(hvMove * Speed * Time.deltaTime);
    }
}