using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour {

    public WaveInfo waveInfo;

    private GameObject playField;

    private int pointsToSpawn;

    private List<float> colorDistribution;
    private float colorDistributionSum;
    private List<float> colorDistrReduxFactors;

    private List<float> typeDistribution;
    private float typeDistributionSum;
    private List<float> typeDistrReduxFactors;

    private float distrSumBoundary = 1f;

    private List<EnemyInfo> enemies;
    private List<Rigidbody2D> enemyPrefabs;

    //for buff testing
    public List<GenericEnemy> gEnemies;

    private void Start () {
        gEnemies = new List<GenericEnemy>();

        //connect to playfield
        playField = GameObject.Find("PlayField");

        //load info from wave data
        pointsToSpawn = waveInfo.pointPool;

        colorDistribution = waveInfo.GetColorDistribution();
        colorDistributionSum = CalculateDistributionSum(colorDistribution);
        colorDistrReduxFactors = InitializeDistrReduxFactors(colorDistribution, colorDistributionSum);

        typeDistribution = waveInfo.GetTypeDistribution();
        typeDistributionSum = CalculateDistributionSum(typeDistribution);
        typeDistrReduxFactors = InitializeDistrReduxFactors(typeDistribution, typeDistributionSum);

        enemies = waveInfo.enemyPool;
        enemyPrefabs = new List<Rigidbody2D>();
        enemies.ForEach((i) => enemyPrefabs.Add(i.enemy));

        //start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    //returns 1/sum * distr
    private List<float> InitializeDistrReduxFactors(List<float> distr, float sum)
    {
        List<float> distrRedFacs = new List<float>();
        foreach (float t in distr) distrRedFacs.Add(t / sum);
        return distrRedFacs;
    }

    //return sum of all elements for given distr
    private float CalculateDistributionSum(List<float> distribution)
    {
        float sum = 0f;

        foreach (float d in distribution)
        {
            sum += d;
        }
        return sum;
    }

    //returns semi-random int from 0 to number of distr entries so that it satisfies set distr
    private int CalculateByDistribution(List<float> distribution, List<float> distrReduxFactors, ref float distrSum)
    {
        int tmp = Random.Range(0, distribution.Count);
        float rnd = Random.value * distrSum;
        for (int i = 0; i < distribution.Count; i++)
        {
            rnd -= distribution[i];

            if (rnd <= float.Epsilon)
            {
                tmp = i;

                distribution[i] *= distrReduxFactors[i];
                distrSum = CalculateDistributionSum(distribution);

                break;
            }
        }
        if (distrSum < distrSumBoundary)
        {
            for (int i = 0; i < distribution.Count; i++)
            {
                distribution[i] *= 10f;
            }
            distrSum = CalculateDistributionSum(distribution);
        }

        return tmp;
    }

    IEnumerator SpawnEnemies () {
        while (true)
        {
            int type = CalculateByDistribution(typeDistribution, typeDistrReduxFactors, ref typeDistributionSum);
            if (enemies[type].pointValue <= pointsToSpawn)
            {
                SpawnEnemy(type);
                yield return new WaitForSeconds(Random.Range(2f, 3f));
            }
            else
            {
                typeDistribution.RemoveAt(type);
                typeDistrReduxFactors.RemoveAt(type);
                typeDistributionSum = CalculateDistributionSum(typeDistribution);
                if (typeDistribution.Count <= 0 || typeDistrReduxFactors.Count <= 0) break;
            }
        }
    }

    //spawns one enemy of type given by it's position in type list
    void SpawnEnemy(int typeIndex)
    {
        EnemyInfo enemyInfo = enemies[typeIndex];
        Rigidbody2D currEnemy = Instantiate(enemyPrefabs[typeIndex]);

        // The value 3f represents the scaling factor of the enemies, that is, the enemy sprites are created for 3 lanes,
        // and we are trying to scale accordingly
        //currEnemy.transform.localScale = new Vector3(3f / playField.GetComponent<PlayField>().rowCount, 3f / playField.GetComponent<PlayField>().rowCount);

        GenericEnemy gEnemy = currEnemy.GetComponent<GenericEnemy>();

        //Set lane randomly
        gEnemy.SetLane(Random.Range(0, playField.GetComponent<PlayField>().rowCount));

        //Set color based on level color distribution
        RGBColor color = (RGBColor)CalculateByDistribution(colorDistribution, colorDistrReduxFactors, ref colorDistributionSum);
        gEnemy.SetBaseColor(color);

        //Set speed based on enemy data
        gEnemy.SetSpeed(enemyInfo.speed);

        pointsToSpawn -= enemyInfo.pointValue;
    }
}

[CustomEditor(typeof(EnemySpawner))]
public class TestSpawnerEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        //This draws the default screen.  You don't need this if you want
        //to start from scratch, but I use this when I'm just adding a button or
        //some small addition and don't feel like recreating the whole inspector.
        if (GUILayout.Button("Buff enemies!")) {
            foreach (GenericEnemy enemy in ((EnemySpawner) target).gEnemies) {
                Dictionary<StatEnum, float> buffMult = new Dictionary<StatEnum, float>();
                buffMult[(int) StatEnum.SPEED] = Random.Range(-0.8f, 1f);
                Buff buff = new Buff(buffMult, Random.Range(0.5f, 2f));
                enemy.ApplyBuff(buff);
            }
        }
    }
}