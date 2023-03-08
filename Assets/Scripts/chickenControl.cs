using UnityEngine;
using UnityEngine.InputSystem;

public class chickenControl : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;

    [SerializeField]
    private float runSpeed = 10f;

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

    public bool isRunning;

    private Vector2 move = new Vector2(0, 0);

    CharacterController characterController;

    float countJump = 0f;

    Vector3 originalPos;

    //private InputActionReference actionReference;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;

        originalPos = transform.position;
    }

    void Update()
    {
        Movement();

        if (characterController.isGrounded)
        {
            if (isRunning)
            {
                speed = runSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
        }

        else
        {
            if (!isRunning)
            {
                speed = walkSpeed;
            }
        }

    }

    
    // MOVEMENT //

    // Get Move Position
    public void OnMove(InputAction.CallbackContext WASD)
    {
        move = WASD.ReadValue<Vector2>();
    }



    // Sprint Function
    public void OnSpeedUp(InputAction.CallbackContext theSpeed)
    {
        if (theSpeed.started)
        {
            isRunning = true;
            Debug.Log("Shift Pressed");
        }

        if (theSpeed.canceled)
        {
            isRunning = false;
            Debug.Log("Shift Released");
        }
    }

    // Movement Function
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

        if (characterController.isGrounded)
        {
            verticalMove = 0;
        }
    }

    // Jump Function
    public void OnJump(InputAction.CallbackContext theJump)
    {
        if (theJump.started)
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

            if (countJump == 2) // Glide function, not yet done
            {
                Debug.Log("Wait untill ground");
                verticalMove -= gravity * Time.deltaTime;
                //characterController.Move(verticalMove * Time.deltaTime);
            }
        }
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
        characterController.enabled = false;
        transform.position = originalPos;
        characterController.enabled = true;
    }

    void GetHit()
    {
        Debug.Log("You got hit");
        transform.position = originalPos;
    }
}