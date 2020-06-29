using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : GenericEnemy {

    public List<AudioClip> spawnClips;

    protected override void Start () {
        base.Start();

        AudioManager.instance.PlaySound(spawnClips[Random.Range(0, spawnClips.Count)]);

        //Set health color
        hpStackList.Add(new HPStack(baseColor, 5));
        hpStackList[0].SetOnDestroy(() => { Debug.Log("Gotov sam"); });

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
