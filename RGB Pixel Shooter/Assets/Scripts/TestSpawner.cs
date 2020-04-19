using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour {

    public Sprite enemySprite;

    private void Start () {
        GameObject enemy = new GameObject("omg zombie");
        TestEnemy tEnemy = enemy.AddComponent<TestEnemy>();
        SpriteRenderer sr = enemy.AddComponent<SpriteRenderer>();

        sr.sprite = enemySprite;

        tEnemy.AddTrait(new ShieldTrait(RGBColor.RED, 2));
        tEnemy.AddHPStack(new HPStack(RGBColor.GREEN));
    }
}
