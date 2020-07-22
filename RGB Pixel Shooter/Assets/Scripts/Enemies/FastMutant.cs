using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMutant : GenericEnemy
{

    public GameObject HP;

    [Header("Sound effects")]
    public List<AudioClip> spawnEffects;
    public List<AudioClip> deathEffects;
    private float vocalPitch;

    protected override void Start()
    {
        base.Start();

        vocalPitch = Random.Range(0.8f, 1.5f);

        if (spawnEffects.Count > 0)
        {
            AudioManager.GetInstance().PlaySoundPitched(spawnEffects[Random.Range(0, spawnEffects.Count)], vocalPitch);
        }

        //Set health color
        hpStackList.Add(new HPStack(baseColor, 50));
        hpStackList[0].SetHPBar(HP);

        //Initiate moving
        Move();
    }

    protected override void Move()
    {
        if (!isDead)
        {
            rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);

        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected override void Die()
    {
        AudioManager.GetInstance().PlaySoundPitched(deathEffects[Random.Range(0, deathEffects.Count)], vocalPitch, true);

        base.Die();
    }

    protected override void InitiateShanking()
    {
        rb.velocity = Vector2.zero;
    }

}
