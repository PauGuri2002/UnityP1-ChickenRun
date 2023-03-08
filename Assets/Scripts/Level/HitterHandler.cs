using UnityEngine;

public class HitterHandler : MonoBehaviour
{
    [SerializeField]
    private float pushForce = 30f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerPhysics pp = collision.gameObject.GetComponent<PlayerPhysics>();
        
        if (pp != null)
        {
            for(int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint point = collision.GetContact(i);
                pp.GetHit(rb.GetPointVelocity(point.point) * pushForce, point.point);
            }
        }
    }
}
