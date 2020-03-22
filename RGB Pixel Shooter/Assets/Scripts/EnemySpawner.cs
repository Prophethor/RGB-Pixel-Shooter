using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Rigidbody2D enemy;
    public float minTimeBetweeenEnemies = 1f;
    public float maxTimeBetweeenEnemies = 3f;
    private int enemiesSpawned = 0;
    public void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        while(enemiesSpawned < GameBehaviour.maxEnemies) {
            Vector3 enemyPos = transform.position + new Vector3(0, Random.Range(-1, 2) * 100f, 0);
            Instantiate(enemy, enemyPos, Quaternion.identity);
            enemiesSpawned++;
            yield return new WaitForSeconds(Random.Range(minTimeBetweeenEnemies,maxTimeBetweeenEnemies));
        }
    }
}
