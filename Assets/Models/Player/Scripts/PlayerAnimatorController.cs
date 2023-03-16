using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorController : MonoBehaviour
{

    private Animator _animator;
    private CharacterController characterController;
    //private bool isFlying;

    void Start()
    {
        _animator = this.GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

    }

    public void OnMove(InputAction.CallbackContext WASD)
    {
        _animator.SetBool("walking", WASD.ReadValue<Vector2>() == new Vector2(0f, 0f) ? false : true);
        //Flying();
    }

    // Sprint Function
    public void OnSpeedUp(InputAction.CallbackContext theSpeed)
    {
        _animator.SetBool("running", isRunning(theSpeed));
    }
    public void OnJump(InputAction.CallbackContext theJump)
    {

        _animator.SetBool("jumping", characterController.isGrounded);
        //Flying();
    }

    public bool isRunning(InputAction.CallbackContext theSpeed)
    {
        if(theSpeed.canceled) {
            return false;
        }
        return true;
    }

    public void Flying()
    {
        //this._animator.SetBool("flying", true);
        _animator.SetBool("flying", true);
    }
}