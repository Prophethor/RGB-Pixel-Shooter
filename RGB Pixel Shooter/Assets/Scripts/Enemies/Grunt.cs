using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : GenericEnemy
{

    public GameObject HP;

    protected override void Start () {
        base.Start();

        //Set health color
        hpStackList.Add(new HPStack(baseColor, 200));
        hpStackList[0].SetHPBar(HP);

    }

    protected override void Move () {
        rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);
        if (isDead)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Stop () {
        rb.velocity = Vector2.zero;
    }
    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }
}
