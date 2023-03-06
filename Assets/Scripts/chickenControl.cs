using UnityEngine;
using UnityEngine.InputSystem;

public class chickenControl : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2f;
    [SerializeField]
    private float runSpeed = 5f;

    [SerializeField]
    private float rotationSens = 5f;

    [SerializeField]
    private float gravity = 9.81f;

    [SerializeField]
    private float highJump = 5f;

    [SerializeField]
    private float pushForce = 10f;

    [SerializeField]
    public GameObject cam;

    private float speed;

    float verticalMove;

    private Vector2 move = new Vector2(0, 0);

    CharacterController characterController;

    float countJump = 0f;

    private float Xrotation = 0f, Yrotation= 0f;

    Vector2 LookPos;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;
    }

    void Update()
    {
        Movement();
        playerLook();
    }

    // CAMERA CONTROL //

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

    // MOVEMENT //

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
    //        speed = walkSpeed;
    //    }
    //    if (isPressed.canceled)
    //    {
    //        speed = runSpeed;
    //    }
    //}

    void Movement()
    {
        // gravity
        verticalMove -= gravity * Time.deltaTime;

        //camera direction
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 horizontalMove = forward * move.y + right * move.x;

        Vector3 hvMove = new Vector3(horizontalMove.x * speed, verticalMove, horizontalMove.z * speed);
        characterController.Move(hvMove * Time.deltaTime);
    }

    // PHYSICS & COLLISION //

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Hitter"))
        {
            GetHit();
        }

        if (hit.gameObject.CompareTag("Killer"))
        {
            Die();
        }

        if (hit.rigidbody == null || hit.rigidbody.isKinematic) { return; }
        hit.rigidbody.AddForceAtPosition(hit.moveDirection * pushForce, hit.point);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hitter"))
        {
            GetHit();
        }

        if (other.gameObject.CompareTag("Killer"))
        {
            Die();
        }
    }

    // DAMAGE & DEATH //

    Coroutine rigidbodyCoroutine;

    void Die()
    {
        Debug.Log("You died");
    }

    void GetHit()
    {
        Debug.Log("You got hit");
    }
}