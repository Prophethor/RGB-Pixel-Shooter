using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using System.Diagnostics;

[Serializable]
public class HPStack {
    private RGBColor color;
    private int maxAmount;
    private float amount;

    private float wrongColorMultiplier = 1f;
    private float noneColorMultiplier = 1f;
    private float correctColorMultiplier = 5f;

    private int hpRegen;
    private float hpRegenTime;
    private float hpRegenCurrentTime;

    private int damageReduction;
    private int threshold;

    private Callback onDestroy;

    public HPStack (RGBColor color, int maxAmount = 1, int hpRegen = 0, float hpRegenTime = 0f, int damageReduction = 0, int threshold = 0) {
        this.color = color;
        this.maxAmount = maxAmount;
        amount = maxAmount;

        this.hpRegen = hpRegen;
        this.hpRegenTime = hpRegenTime;
        hpRegenCurrentTime = hpRegenTime;

        this.damageReduction = damageReduction;
        this.threshold = threshold;
    }

    public void SetOnDestroy (Callback callback) {
        onDestroy = callback;
    }

    public void Update (float deltaTime) {
        hpRegenCurrentTime -= deltaTime;

        if (hpRegenCurrentTime <= 0f) {
            amount = Math.Min(amount + hpRegen, maxAmount);
            hpRegenCurrentTime = hpRegenTime;
        }
    }

    public bool TakeDamage (RGBDamage damage, out HitStatus hitStatus) {
        hitStatus = new HitStatus();
        bool tempBool = false;
        //----------------------------First we check the how the color matches up------------

        if (damage.color == color) { // ako je pogodio tacnu boju metka
            hitStatus.hitColor = HitColor.CORRECT;
            hitStatus.damageAmount = damage.amount * correctColorMultiplier;
        }
        else if (damage.color == RGBColor.NONE) { // ako je neutralni metak
            hitStatus.hitColor = HitColor.NEUTRAL;
            hitStatus.damageAmount = damage.amount * noneColorMultiplier;
        }
        else {  // ako je promasio boju a NIJE Neutralni metak
            hitStatus.hitColor = HitColor.WRONG;
            hitStatus.damageAmount = damage.amount * wrongColorMultiplier;
        }

        hitStatus.damageAmount -= damageReduction;
        tempBool = true;

        //------------------- Below we chech the trehold and armor pass marks--------------------
        if (hitStatus.damageAmount < threshold) {
            hitStatus.belowThreshold = true;
            hitStatus.damageAmount = 0f;
            tempBool = false;
        }

        //Debug.Log("  HitStatus===" + tempHit + " HitBool==" + tempBool + "===DMG==" + tempAmount);
        amount -= hitStatus.damageAmount;

        if (amount <= 0f && onDestroy != null) {
            onDestroy();
        }

        return tempBool;
    }

    public RGBColor GetColor () {
        return color;
    }

    public float GetAmount () {
        return amount;
    }

}