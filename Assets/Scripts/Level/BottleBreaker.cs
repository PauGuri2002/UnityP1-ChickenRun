using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleBreaker : MonoBehaviour
{
    [SerializeField] private float terminalVelocity = 20f;
    [SerializeField] private GameObject particles;
   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude >= terminalVelocity)
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
