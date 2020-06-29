using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : GenericEnemy {

    public GameObject HP;

    protected override void Start () {
        base.Start();

        //Set health color
        hpStackList.Add(new HPStack(baseColor, 100));
        hpStackList[0].SetHPBar(HP);
        //Initiate moving
        Move();
    }

    protected override void Move () {
        if (!isDead)
        {
            rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);

        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
   
    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }

}
