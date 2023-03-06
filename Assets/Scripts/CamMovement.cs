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

    }

    // Update is called once per frame
    void Update()
    {

        //

    }

    void Move()
    {
        transform.position = position;
    }

    public void ToggleCam(){

        thirdperson = thirdperson? false : true;

        if (thirdperson == false)
        {
            position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
        else
        {
            position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 10);
        }
        Move();

    }
}
