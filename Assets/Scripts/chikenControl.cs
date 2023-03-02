using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class chikenControl : MonoBehaviour
{
    [SerializeField]
    private float Speed = 2f;

    [SerializeField]
    private float personalForce = 300f;

    private Vector2 move = new Vector2(0, 0);

    Rigidbody gravity;

    bool isgrounded;

    float countJump = 0f;



    // Start is called before the first frame update
    void Start()
    {
        gravity = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move.x * Speed * Time.deltaTime, 0, move.y * Speed * Time.deltaTime, Space.World);

        if (countJump == 2 && Input.GetKey(KeyCode.Space))
        {   
            gravity.velocity = 0.95f * gravity.velocity;

        }
    }

    void OnMove(InputValue WASD)
    {
        move = WASD.Get<Vector2>();
    }

    void OnJump()
    {
        if (isgrounded == true || countJump < 2)
        {
            gravity.AddForce(0, personalForce, 0);
            countJump++;
            Debug.Log("jumpCount++");

        }
    }

    void OnSpeedUp()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
            countJump = 0;
            Debug.Log("jumpCount Reset");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = false;
        }
    }
}