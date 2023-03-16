using UnityEngine;
using UnityEngine.InputSystem;

public class chickenControl : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float highJump = 5f;
    [SerializeField] private float glideForce = 5f;

    [SerializeField] private GameObject cam;

    [SerializeField] private GameObject door;
    private Animator _doorAnimator;
    private Animator _playerAnimator;

    //PlayerAnimatorController _callAnimator;

    CharacterController characterController;

    public float speed;

    private float verticalMove;

    private Vector2 move = new Vector2(0, 0);

    float countJump = 0f;

    private bool isJumped;
    private bool isRunning;
    private bool isFlying;

    private bool key = false;
    private bool openDoorAvailability = false;

    private bool apexTigger;
    private float apexLastFrame;

    [SerializeField] private GameObject modelKFC;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        _doorAnimator = door.GetComponent<Animator>();

        speed = walkSpeed;

        _playerAnimator = this.GetComponentInChildren<Animator>();
        //_callAnimator = GetComponent<PlayerAnimatorController>();
        isFlying = false;

    }

    void Update()
    {
       // Debug.Log(_playerAnimator);
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

        apexLastFrame = verticalMove;

        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, Camera.main.transform.localEulerAngles.y);

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
            //Debug.Log("Shift Pressed");
        }

        if (theSpeed.canceled)
        {
            isRunning = false;
            //Debug.Log("Shift Released");
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
                //_callAnimator.Flying();
            }
            countJump++;

            if(countJump == 2)
            {
                isFlying = true;
            }
        }

        if (theJump.canceled)
        {
            isJumped = false;
        }
    }

    public void OnPickUp()
    {
        if (this.openDoorAvailability == false)
        {
            return;
        }
        if (this.key)
        {
            OpenFinalDoor();
        }
    }

    void Movement()
    {   
        //transform.rotation = Quaternion.identity;

        //Debug.Log(apexLastFrame + " " + verticalMove);
        if (characterController.enabled == false) { return; }

        // Glide function
        if (apexTigger == true && isJumped == true)
        {
                    
            //Debug.Log("Wait untill ground");
            verticalMove -= glideForce * Time.deltaTime;

            //Flying();

            //this._playerAnimator.SetBool("flying", isFlying);
            //_callAnimator.Flying();
            if (isFlying == true)
            {
                Debug.Log("Tiene que estar volando xD");
                Flying();
            }

        }

        else
        {
            verticalMove -= gravity * Time.deltaTime;
            //isFlying = false;
            //Flying();
        }

        if (verticalMove * apexLastFrame < 0 && countJump == 2)
        {
            apexTigger = true;
            //Debug.Log("esta entrando");
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

        if (horizontalMove.magnitude > 0) { modelKFC.transform.rotation = Quaternion.LookRotation(horizontalMove); }

        // reset all properties when grouded
        if (characterController.isGrounded)
        {
            verticalMove = 0;
            apexTigger = false;
            countJump = 0;
        }
    }

    private void OpenFinalDoor()
    {
        this._doorAnimator.SetBool("open", true);
    }

    public void SetKey(bool value)
    {
        this.key = value;
    }

    public void SetOpenDoorAvailability(bool value)
    {
        this.openDoorAvailability = value;
    }

    public void Flying()
    {
        //this._playerAnimator.SetBool("flying", true);
        _playerAnimator.SetBool("flying", true);
    }
}