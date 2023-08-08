using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpawner : MonoBehaviour
{
    public GameObject acid;
    public float spawnRate = 2f;

    private float nextSpawn = 0f;
    private float randX;
    private Vector2 whereToSpawn;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(transform.position.x - 3, transform.position.x + 3);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(acid, whereToSpawn, Quaternion.identity);
        }
    }
}
