using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField]
    private float pushForce = 10f;
    [SerializeField]
    private float recoverTime = 2f;

    private Vector3 originalPos;

    private CharacterController characterController;
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;

        originalPos = transform.position;
    }

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

    Coroutine ragdollCoroutine;

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
        if (ragdollCoroutine == null)
        {
            ragdollCoroutine = StartCoroutine(HandleRagdoll());
        }
    }

    IEnumerator HandleRagdoll()
    {
        characterController.enabled = false;
        capsuleCollider.enabled = true;
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = pushForce;

        yield return new WaitForSeconds(recoverTime);

        Destroy(rb);
        capsuleCollider.enabled = false;
        characterController.enabled = true;
    }
}
