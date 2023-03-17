using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorController : MonoBehaviour
{

    private Animator _animator;
    private CharacterController characterController;

    void Start()
    {
        _animator = this.GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext WASD)
    {
        _animator.SetBool("walking", WASD.ReadValue<Vector2>() == new Vector2(0f, 0f) ? false : true);
    }

    // Sprint Function
    public void OnSpeedUp(InputAction.CallbackContext theSpeed)
    {
        _animator.SetBool("running", isRunning(theSpeed));
    }
    public void OnJump(InputAction.CallbackContext theJump)
    {
        if (theJump.started){
            _animator.SetBool("jumping",true);
        } else if (theJump.canceled)
        {
            _animator.SetBool("jumping", false);
        }
        
    }

    public bool isRunning(InputAction.CallbackContext theSpeed)
    {
        if(theSpeed.canceled) {
            return false;
        }
        return true;
    }
}