using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : GenericEnemy {

    protected override void Start () {
        base.Start();

        //Set health color
        hpStackList.Add(new HPStack(baseColor, 1));

        //Initiate moving
        Move();
    }

    protected override void Move () {
        rb.velocity = new Vector2(-speed, 0);
    }

    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }

}
