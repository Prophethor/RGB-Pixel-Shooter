using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : GenericEnemy
{

    public AnimatorOverrideController overrideRed;
    public AnimatorOverrideController overrideBlue;
    public AnimatorOverrideController overrideGreen;


    public GameObject HP;


    protected override void Start () {
        base.Start();

        AudioManager.instance.PlaySoundPitched(spawnClips[Random.Range(0, spawnClips.Count)], 2f);

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
