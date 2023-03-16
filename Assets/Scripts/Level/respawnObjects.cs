using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnObjects : MonoBehaviour
{
    private Dictionary <GameObject,Vector3> positionFix;
    private Dictionary<GameObject, Quaternion> rotationFix;

    void Start()
    {   
        positionFix = new Dictionary <GameObject,Vector3>();
        rotationFix = new Dictionary<GameObject, Quaternion>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            positionFix[obj] = (obj.transform.position);
            rotationFix[obj] = (obj.transform.rotation);
        }
    }

    public void ResetTransform()
    {   

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            obj.transform.position = positionFix[obj];
            obj.transform.rotation = rotationFix[obj];
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
        }
    }
}
