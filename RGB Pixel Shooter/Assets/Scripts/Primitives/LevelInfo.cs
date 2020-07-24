using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level"), System.Serializable]
public class LevelInfo : ScriptableObject {
    [Range(0f, 10f)]
    public float redDistribution = 10f;

    [Range(0f, 10f)]
    public float greenDistribution = 10f;

    [Range(0f, 10f)]
    public float blueDistribution = 10f;

    public List<WaveInfo> waves;
    public AudioClip soundtrack;

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
}
