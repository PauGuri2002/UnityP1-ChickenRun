using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WindowBreaker : MonoBehaviour
{
    [SerializeField]
    private GameObject particles;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject particleInstance = Instantiate(particles, transform.position, Quaternion.Euler(0,-90,0));
        ShapeModule particleShape = particleInstance.GetComponent<ParticleSystem>().shape;
        particleShape.scale = transform.localScale;
        Destroy(gameObject);
    }
}
