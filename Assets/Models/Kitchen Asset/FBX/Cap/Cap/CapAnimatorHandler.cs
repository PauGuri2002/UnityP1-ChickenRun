using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapAnimator : MonoBehaviour
{

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)

    {
        chickenControl player = other.GetComponent<chickenControl>();
        Debug.Log("HEY");
        if (player != null)
        {
            _animator.SetBool("openCap", true);
        }
        
    }

    private void OnTriggerExit(Collider other)

    {
        chickenControl player = other.GetComponent<chickenControl>();
        Debug.Log("HEY2");
        if (player != null)
        {
            _animator.SetBool("openCap", false);
        }

    }

}
