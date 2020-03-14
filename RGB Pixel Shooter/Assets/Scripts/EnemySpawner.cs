using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Rigidbody2D enemy;
    public void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        for (; ; ) {
            Vector3 enemyPos = transform.position + new Vector3(0, Random.Range(-1, 2) * 100f, 0);
            Instantiate(enemy, enemyPos, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
}
