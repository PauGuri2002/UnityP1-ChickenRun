using System.Collections.Generic;
using UnityEngine;

public class respawnObjects : MonoBehaviour
{
    private Dictionary <GameObject,ObjectProperties> respawnableObjects;

    void Start()
    {   
        respawnableObjects = new Dictionary <GameObject,ObjectProperties>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            respawnableObjects[obj] = new ObjectProperties(obj.transform.position, obj.transform.rotation);
        }
    }

    public void ResetTransform()
    {   

        foreach(KeyValuePair<GameObject,ObjectProperties> storedObj in respawnableObjects)
        {
            GameObject obj = storedObj.Key;
            obj.SetActive(true);
            obj.transform.position = respawnableObjects[obj].position;
            obj.transform.rotation = respawnableObjects[obj].rotation;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private class ObjectProperties
    {
        public Vector3 position;
        public Quaternion rotation;

        public ObjectProperties(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
}
