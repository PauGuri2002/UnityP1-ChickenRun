using UnityEngine;

public class KinematicToDynamic : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
