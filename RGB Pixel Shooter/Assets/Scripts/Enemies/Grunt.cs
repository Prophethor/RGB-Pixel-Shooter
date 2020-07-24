using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : GenericEnemy {

    public GameObject HP;

    [Header("Sound effects")]
    public List<AudioClip> spawnEffects;
    public List<AudioClip> deathEffects;
    public List<AudioClip> damageEffects;
    private float vocalPitch;

    protected override void Start () {
        base.Start();

        vocalPitch = Random.Range(0.5f, 1.2f);

        if (spawnEffects.Count > 0) {
            AudioManager.GetInstance().PlaySoundPitched(spawnEffects[Random.Range(0, spawnEffects.Count)], vocalPitch);
        }

        //Set health color
        hpStackList.Add(new HPStack(baseColor, 200));
        hpStackList[0].SetHPBar(HP);

    }

    protected override void Update()
    {
        base.Update();
        //Move();
    }

    public override void TakeDamage (RGBDamage damage) {
        base.TakeDamage(damage);

        AudioManager.GetInstance().PlaySoundPitched(damageEffects[Random.Range(0, damageEffects.Count)], vocalPitch);
    }

    protected override void Move () {
        rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);
        if (isDead) {
            rb.velocity = Vector2.zero;
        }
    }

    public void Stop () {
        rb.velocity = Vector2.zero;
    }

    protected override void Die () {
        AudioManager.GetInstance().PlaySoundPitched(deathEffects[Random.Range(0, deathEffects.Count)], vocalPitch, true);

        base.Die();
    }

    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }
}
