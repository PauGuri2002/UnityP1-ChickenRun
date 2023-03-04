using UnityEngine;

public class WindowBreaker : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }
        particles.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
