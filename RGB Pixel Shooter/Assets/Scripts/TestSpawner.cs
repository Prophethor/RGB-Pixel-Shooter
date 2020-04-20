using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour {

    public Rigidbody2D enemy;
    public int enemyCount = 15;
    private void Start () {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyCount > 0)
        {
            Rigidbody2D currEnemy = Instantiate(enemy);
            currEnemy.GetComponent<Mutant>().laneSetter = Random.Range(0,3);
            enemyCount--;
            yield return new WaitForSeconds(2f);
        }
        
    }
}
