using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapAnimatorHandler : MonoBehaviour
{

    private Animator _animator;

    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)

    {
        chickenControl player = other.GetComponent<chickenControl>();
        if (player != null)
        {
            _animator.SetBool("openCap", true);
        }
        
    }

    private void OnTriggerExit(Collider other)

    {
        chickenControl player = other.GetComponent<chickenControl>();
        if (player != null)
        {
            _animator.SetBool("openCap", false);
        }

    }

}
