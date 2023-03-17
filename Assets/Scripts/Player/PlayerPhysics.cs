using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private float recoverTime = 2f;

    private Vector3 respawnPos;

    private CharacterController characterController;
    private CapsuleCollider capsuleCollider;
    private chickenControl chickenControl;
    private respawnObjects respawnObjectsScript;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        chickenControl = GetComponent<chickenControl>();
        respawnObjectsScript = GetComponent<respawnObjects>();

        respawnPos = transform.position;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Killer"))
        {
            Die();
        }

        if (hit.rigidbody == null || hit.rigidbody.isKinematic) { return; }
        hit.rigidbody.AddForceAtPosition(hit.moveDirection * pushForce, hit.point);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Killer"))
        {
            Die();
        }
        if (other.gameObject.CompareTag("FinalDoor"))
        {
            this.chickenControl.SetOpenDoorAvailability(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FinalDoor"))
        {
            this.chickenControl.SetOpenDoorAvailability(false);
        }
    }

    public void SetRespawnPoint(Vector3 pos)
    {
        respawnPos = pos;
    }
    public void SetRespawnPoint(Transform trans)
    {
        respawnPos = trans.position;
    }

    // DAMAGE & DEATH //

    Coroutine ragdollCoroutine;
    Rigidbody rb;

    public void Die()
    {
        Debug.Log("You died");
        characterController.enabled = false;
        transform.position = respawnPos;
        characterController.enabled = true;
        respawnObjectsScript.ResetTransform();
    }

    public void GetHit(Vector3 hitForce, Vector3 hitPoint)
    {
        Debug.Log("You got hit");
        if (ragdollCoroutine == null)
        {
            ragdollCoroutine = StartCoroutine(HandleRagdoll(hitForce,hitPoint));
        } else if(rb != null)
        {
            HandleHit(hitForce, hitPoint);
        }
    }

    IEnumerator HandleRagdoll(Vector3 hitForce, Vector3 hitPoint)
    {
        characterController.enabled = false;
        capsuleCollider.enabled = true;
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = pushForce;

        HandleHit(hitForce, hitPoint);

        yield return new WaitForSeconds(recoverTime);

        Destroy(rb);
        capsuleCollider.enabled = false;
        characterController.enabled = true;
        ragdollCoroutine = null;
    }

    private void HandleHit(Vector3 hitForce, Vector3 hitPoint)
    {
        rb.AddForceAtPosition(hitForce, hitPoint);
    }
}
