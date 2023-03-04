using UnityEngine;

public class MapCamFollower : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float verticalOffset = 10f;
    [SerializeField]
    private bool followRotation = true; 

    void Update()
    {
        transform.position = new Vector3(player.position.x, verticalOffset, player.position.z);
        transform.rotation = Quaternion.Euler(90, player.rotation.eulerAngles.y, 0);
    }
}
