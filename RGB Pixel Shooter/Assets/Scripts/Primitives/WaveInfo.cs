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

    public List<float> GetTypeDistribution () {
        List<float> tmp = new List<float>();
        foreach (float d in typeDistr) tmp.Add(d);
        return tmp;
    }
}
