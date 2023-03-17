using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)

    {
        chickenControl player = other.GetComponent<chickenControl>();
        if (player != null)
        {
            AddKeyToPlayer(player);
        }

    }

    private void AddKeyToPlayer(chickenControl player)
    {
        player.SetKey(true);
        Destroy(gameObject);
    }
}