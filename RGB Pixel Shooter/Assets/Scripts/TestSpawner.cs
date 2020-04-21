using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour {

    public Rigidbody2D enemy;
    public int enemyCount = 15;

    private GameObject playField;
    private void Start () {
        playField = GameObject.Find("PlayField");
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyCount > 0)
        {
            Rigidbody2D currEnemy = Instantiate(enemy);
            currEnemy.transform.localScale = new Vector3(3f / playField.GetComponent<PlayField>().rowCount, 3f / playField.GetComponent<PlayField>().rowCount);
            currEnemy.GetComponent<Mutant>().laneSetter = Random.Range(0,playField.GetComponent<PlayField>().rowCount);
            enemyCount--;
            yield return new WaitForSeconds(2f);
        }
        
    }
}
