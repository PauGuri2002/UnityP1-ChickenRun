using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnObjects : MonoBehaviour
{
    private Dictionary <GameObject,Vector3> respawnObj;
    // Start is called before the first frame update
    void Start()
    {   
        respawnObj = new Dictionary <GameObject,Vector3>();

        foreach (GameObject emptyPosition in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            respawnObj[emptyPosition] = (emptyPosition.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void itsALIVEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE()
    {   

        foreach(GameObject emptyPosition in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            emptyPosition.transform.position = respawnObj[emptyPosition];
            Rigidbody positionFIXXXXXXXXXXXXXXXXXXXXXXXXED = emptyPosition.GetComponent<Rigidbody>();
            positionFIXXXXXXXXXXXXXXXXXXXXXXXXED.velocity = Vector3.zero;
        }
    }
}
