using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Wave", menuName = "Wave")]
[System.Serializable]
public class WaveInfo : ScriptableObject {

    public int pointPool;
    public List<EnemyInfo> enemyPool;
    [Range(0f, 10f)]
    public List<float> typeDistr;
    [Range(0f, 10f)]
    public float redDistribution;

    [Range(0f, 10f)]
    public float greenDistribution;

    [Range(0f, 10f)]
    public float blueDistribution;


    public List<float> GetTypeDistribution()
    {
        List<float> tmp = new List<float>();
        foreach (float d in typeDistr) tmp.Add(d);
        return tmp;
    }

    public List<float> GetColorDistribution () {
        List<float> tmp = new List<float>();
        tmp.Add(redDistribution);
        tmp.Add(greenDistribution);
        tmp.Add(blueDistribution);
        return tmp;
    }

    public float GetColorDistributionSum () {
        return redDistribution + greenDistribution + blueDistribution;
    }
    

    public WaveInfo () {

    }
}
