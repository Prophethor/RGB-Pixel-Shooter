using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : GenericEnemy
{
    protected override void Start () {
        base.Start();

        //Set health color
        hpStackList.Add(new HPStack(baseColor, 2));

    }

    protected override void Move () {
        rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);
    }

    public void Stop () {
        Debug.Log("Stop");
        rb.velocity = Vector2.zero;
    }
    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }
}
