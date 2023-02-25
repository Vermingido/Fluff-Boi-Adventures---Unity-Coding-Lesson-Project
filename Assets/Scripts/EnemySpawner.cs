using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float timeToWait;
    private float timeSinceSpawned;

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyToSpawn,transform.position,transform.rotation);
        timeSinceSpawned = 0;
    }

    private void Update()
    {
        timeSinceSpawned += Time.deltaTime;
        if(timeSinceSpawned > timeToWait)
        {
            SpawnEnemy();
        }
    }
}