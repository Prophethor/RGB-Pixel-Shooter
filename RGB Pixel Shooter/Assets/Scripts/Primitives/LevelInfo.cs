using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level"), System.Serializable]
public class LevelInfo : ScriptableObject {

    public List<WaveInfo> waves;
}
