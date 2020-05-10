using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemyInfo : ScriptableObject {
    public Rigidbody2D enemy;
    public int pointValue;
    public float speed;
}
