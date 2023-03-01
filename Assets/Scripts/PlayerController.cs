using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int speed_divider = 20;
    private CharacterController Cc;
    private float movementX;
   // private float movementY;
    private float movementZ;
    public float gravity = 9.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cc = GetComponent<CharacterController>();
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
        
        
    }
    void OnJump() {
        Vector3 jump = new Vector3(0, 0, 0);
        if (Cc.isGrounded)
        {
            jump = new Vector3(0, 250* gravity * Time.deltaTime, 0);
            Cc.Move(jump);
        }


    }
    
    void Update(){
        
        Vector3 movement = new Vector3(0,0,0);
        if (!Cc.isGrounded)
        {
            movement = new Vector3(movementX/speed_divider, -gravity * Time.deltaTime, movementZ/speed_divider);
        }
        else
        {
            movement = new Vector3(movementX/speed_divider, 0, movementZ/speed_divider);
            
        }
        //Vector3 point = new Vector3(transform.position.x + movement.x,transform.position.y , transform.position.z + movement.z);
        Cc.Move(movement);
        //if(movement.magnitude > 0)
        //{
        //    transform.LookAt(point);
        //}
        


    }
 
}
