using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fruitPrefabs;
    [SerializeField]
    private Vector2 spawnerSize;
    [SerializeField]
    private float spawnTimeMin = 0.1f;
    [SerializeField]
    private float spawnTimeMax = 0.5f;
    [SerializeField]
    private int maxCount = 20;

    private List<GameObject> spawnedFruit = new List<GameObject>();
    private Coroutine c;

    public void ActivateSpawner()
    {
        if(c != null) { return; }
        c = StartCoroutine(SpawnFruit());
    }

    public void DeactivateSpawner()
    {
        if(c == null) { return; }
        StopCoroutine(c);
        c = null;
    }

    IEnumerator SpawnFruit()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));

            GameObject fruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            Vector3 position = transform.position + new Vector3(Random.Range(-(spawnerSize.x / 2), spawnerSize.x / 2), 0, Random.Range(-(spawnerSize.y / 2), spawnerSize.y / 2));
            spawnedFruit.Add(Instantiate(fruit, position, Quaternion.identity));

            if(spawnedFruit.Count >= maxCount)
            {
                GameObject oldestFruit = spawnedFruit[0];
                spawnedFruit.Remove(oldestFruit);
                Destroy(oldestFruit);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnerSize.x, 0, spawnerSize.y));
    }
}
