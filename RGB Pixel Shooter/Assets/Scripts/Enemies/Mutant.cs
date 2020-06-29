using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : GenericEnemy {


    public AnimatorOverrideController overrideRed;
    public AnimatorOverrideController overrideBlue;
    public AnimatorOverrideController overrideGreen;

    public GameObject HP;


    protected override void Start () {
        base.Start();
        //Set health color

        hpStackList.Add(new HPStack(baseColor, 100));
        hpStackList[0].SetHPBar(HP);
        hpStackList[0].SetOnDestroy(() => { Debug.Log("Gotov sam"); });

        switch (baseColor)
        {
            case RGBColor.RED:
                animator.runtimeAnimatorController = overrideRed;
                break;
            case RGBColor.GREEN:
                animator.runtimeAnimatorController = overrideGreen;
                break;
            case RGBColor.BLUE:
                animator.runtimeAnimatorController = overrideBlue;
                break;
            case RGBColor.NONE:
                break;
            default:
                break;
        }


      

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
