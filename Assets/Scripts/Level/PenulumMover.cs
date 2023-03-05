using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenulumMover : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis = new Vector3(1f,0f,1f);
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private float maxAngle = 45f;
    private float timer = 0f;

    void Update()
    {
        timer = Time.fixedTime * 2 * Mathf.PI * speed;
        float angle = maxAngle * Mathf.Sin(timer);
        transform.rotation = Quaternion.Euler(axis * angle);
    }
}
