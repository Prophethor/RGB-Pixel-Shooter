using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestSpawner : MonoBehaviour {

    public Rigidbody2D enemy;
    public int enemyCount = 15;

    public List<GenericEnemy> enemies;

    private GameObject playField;

    private void Start () {
        playField = GameObject.Find("PlayField");
        enemies = new List<GenericEnemy>();

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy () {
        while (enemyCount > 0) {
            Rigidbody2D currEnemy = Instantiate(enemy);
            currEnemy.transform.localScale = new Vector3(3f / playField.GetComponent<PlayField>().rowCount, 3f / playField.GetComponent<PlayField>().rowCount);
            currEnemy.GetComponent<GenericEnemy>().SetLane(Random.Range(0, playField.GetComponent<PlayField>().rowCount));
            currEnemy.GetComponent<GenericEnemy>().SetSpeed(1f); // Temporary, until EnemyInfo is added
            enemies.Add(currEnemy.GetComponent<GenericEnemy>());
            enemyCount--;
            yield return new WaitForSeconds(3f);
        }

    }
}

[CustomEditor(typeof(TestSpawner))]
public class TestSpawnerEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        //This draws the default screen.  You don't need this if you want
        //to start from scratch, but I use this when I'm just adding a button or
        //some small addition and don't feel like recreating the whole inspector.
        if (GUILayout.Button("Buff enemies!")) {
            foreach (GenericEnemy enemy in ((TestSpawner) target).enemies) {
                Dictionary<StatEnum, float> buffMult = new Dictionary<StatEnum, float>();
                buffMult[(int) StatEnum.SPEED] = Random.Range(-0.8f, 1f);
                Buff buff = new Buff(buffMult, Random.Range(0.5f, 2f));
                enemy.ApplyBuff(buff);
            }
        }
    }
}