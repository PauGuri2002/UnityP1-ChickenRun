using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeCamera : MonoBehaviour
{
    private Vector2 look = Vector2.zero;
    private Vector2 move = Vector2.zero;
    [SerializeField]
    private float speed = 2f;
    private Vector2 rotation = Vector2.zero;

    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation += look;
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);
        transform.localRotation = Quaternion.Euler(-rotation.y, rotation.x, 0);
        transform.Translate(new Vector3(speed * Time.deltaTime * move.x, 0, speed * Time.deltaTime * move.y), Space.Self);
    }
}
