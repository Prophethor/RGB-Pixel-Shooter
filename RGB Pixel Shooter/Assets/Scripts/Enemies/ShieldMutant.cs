﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMutant : GenericEnemy {

    public GameObject HP1;
    public GameObject HP2;

    [Header("Sound effects")]
    public List<AudioClip> spawnEffects;
    public List<AudioClip> deathEffects;
    public AudioClip shieldBreakEffect;
    public AudioClip shieldDeflectEffect;
    private float vocalPitch;

    protected override void Start () {
        base.Start();

        vocalPitch = Random.Range(0.8f, 1.5f);

        if (spawnEffects.Count > 0) {
            AudioManager.GetInstance().PlaySoundPitched(spawnEffects[Random.Range(0, spawnEffects.Count)], vocalPitch);
        }

        RGBColor randcolor = (RGBColor) Random.Range(0, 3);
        hpStackList.Add(new HPStack(randcolor, 100, 0, 0, 0, 100));
        hpStackList[0].SetHPBar(HP1);
        hpStackList.Add(new HPStack(baseColor, 100));
        hpStackList[1].SetHPBar(HP2);
        animator.SetBool("hasShield", true);
        animator.SetTrigger("shield" + randcolor.GetString());
        Move();
    }

    protected override void Move () {
        if (!isDead) {
            rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);

        }
        else {
            rb.velocity = Vector2.zero;
        }
    }

    public override void TakeDamage (RGBDamage damage) {
        if (hpStackList.Count == 0) {
            return;
        }

        sr.material = flashMaterial;
        Tweener.Invoke(0.1f, () => sr.material = defaultMaterial);

        HitStatus hitStatus;
        if (hpStackList[0].TakeDamage(damage, out hitStatus)) {
            hpStackList.RemoveAt(0);
            if (hpStackList.Count == 1) {
                animator.SetBool("hasShield", false);
                animator.SetTrigger("break");

                AudioManager.GetInstance().PlaySound(shieldBreakEffect);
            }
            if (hpStackList.Count == 0) {
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false;
                animator.SetBool("isDead", isDead);
                animator.SetTrigger("is" + baseColor.GetString());
                Tweener.Invoke(.1f, () => sr.color = Color.gray);
                Move();
                Die();
            }
        }
        else if (hitStatus == HitStatus.BELOW_THRESHOLD) {
            animator.SetTrigger("deflect");

            AudioManager.GetInstance().PlaySound(shieldDeflectEffect, true);
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
