using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class HPStack {
    private RGBColor color;
    private int maxAmount;
    private float amount;

    private float wrongColorMultiplier = 0.5f;
    private float noneColorMultiplier = 1f;
    private float correctColorMultiplier = 2f;

    private int hpRegen;
    private float hpRegenTime;
    private float hpRegenCurrentTime;

    private int damageReduction;
    private int threshold;

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

    public void Update (float deltaTime) {
        hpRegenCurrentTime -= deltaTime;

        if (hpRegenCurrentTime <= 0f) {
            amount = Math.Min(amount + hpRegen, maxAmount);
            hpRegenCurrentTime = hpRegenTime;
        }
    }

    public bool TakeDamage(RGBDamage damage, out HitStatus hitStatus) {

        float tempAmount = amount;
        HitStatus tempHit = HitStatus.INSUFFICIENT_DAMAGE;
        bool tempBool = false;
        //----------------------------First we check the how the color matches up------------

        if ((damage.color != color) || (damage.color != RGBColor.NONE)) {  // ako je promasio boju a NIJE Neutralni metak
            tempHit = HitStatus.HIT_WRONG_COLOR;
            tempAmount = damage.amount * wrongColorMultiplier;
            hitStatus = tempHit;
            tempBool = true;

        }
        if (damage.color == RGBColor.NONE) // ako je neutralni metak
        {
            tempHit = HitStatus.HIT_NEUTRAL;
            tempAmount = damage.amount *noneColorMultiplier;
            hitStatus = tempHit;
            tempBool = true;

        }
        if (damage.color == color) // ako je pogodio tacnu boju metka
        {
            tempHit = HitStatus.HIT_CORRECT;
            tempAmount = damage.amount * correctColorMultiplier;
            hitStatus = tempHit;
            tempBool = true;
        }

        //------------------- Below we chech the trehold and armor pass marks--------------------
        if (tempAmount <= damageReduction) // TODO make sure this is significant ammount of damage if it passes
        {
            tempHit = HitStatus.INSUFFICIENT_DAMAGE;
            hitStatus = tempHit;
            tempBool = false;
        }
        if ((tempAmount - damageReduction)< threshold)
        {
            tempHit = HitStatus.BELOW_THRESHOLD;
            hitStatus = tempHit;
            tempBool = false;
        }
        Debug.Log("  HitStatus===" + tempHit + " HitBool==" + tempBool + "===DMG==");
        amount -= tempAmount;
        hitStatus = tempHit;
        return tempBool;
    }

    public RGBColor GetColor () {
        return color;
    }

    public float GetAmount () {
        return amount;
    }

}