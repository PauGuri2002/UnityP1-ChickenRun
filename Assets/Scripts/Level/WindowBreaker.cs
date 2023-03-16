using UnityEngine;

public class WindowBreaker : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;
    AudioSource breakSound;

    private void Start()
    {
        breakSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }
        particles.Play();
        breakSound.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
