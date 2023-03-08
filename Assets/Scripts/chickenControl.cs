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
    private float glideForce = -5f;
    [SerializeField]
    private GameObject cam;

    CharacterController characterController;

    private float speed;

    private float verticalMove;

    private Vector2 move = new Vector2(0, 0);

    float countJump = 0f;

    private bool isJumped;
    private bool isRunning;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;
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
    public void OnJump(InputAction.CallbackContext theJump)
    {
        if (theJump.started)
        {
            isJumped = true;

            if (characterController.isGrounded)
            {
                countJump = 0;
            }

            if (countJump >= 0 && countJump <= 1)
            {
                verticalMove = highJump;
            }
            countJump++;
        }

        if (theJump.canceled)
        {
            isJumped = false;
        }
    }
    void Movement()
    {
        // gravity

        if(characterController.enabled == false) { return; }
        
        if (countJump == 3 && isJumped == true) // Glide function, not yet done
        {
            Debug.Log("Wait untill ground");
            verticalMove = glideForce - Time.deltaTime;
        }
        else
        {
            verticalMove -= gravity * Time.deltaTime;
        }

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
}