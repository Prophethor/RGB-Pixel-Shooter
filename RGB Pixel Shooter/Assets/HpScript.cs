using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpScript : MonoBehaviour
{
    public float maxHp;
    public float currentHP;

    private SpriteRenderer[] renderers;
    private SpriteRenderer hpFill;

    private void Start()
    {
        renderers = this.GetComponentsInChildren<SpriteRenderer>();
        hpFill = renderers[1]; // drugi po redu, prvi je za okvir (taj treba da se swapuje, ovaj ce se gnjeci)
        Initialize();
    }

    private void Initalize()
    {

    }
    public void ApplyDamage(HPStack currentStack, RGBDamage damage)
    {

    }
}
