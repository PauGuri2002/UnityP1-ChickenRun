using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Vector3 position;
    public bool thirdperson = false;

    
    // Start is called before the first frame update
    void Start()
    {
        if (!thirdperson)
        {
            player.GetComponent<MeshRenderer>().enabled = false;
    
        }
    }

    // Update is called once per frame
    void Update()
    {

        
        

    }

    void Move()
    {
        transform.position = position;
    }

    public void ToggleCam(){

        thirdperson = thirdperson? false : true;

        if (thirdperson == false)
        {
            position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
            player.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 10);
            player.GetComponent<MeshRenderer>().enabled = true;


        }
        Move();

    }
}
