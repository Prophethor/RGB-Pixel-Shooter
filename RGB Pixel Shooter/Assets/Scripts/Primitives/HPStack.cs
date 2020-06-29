using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine.UIElements;

[Serializable]
public class HPStack {
    private RGBColor color;
    private int maxAmount;
    private int amount;

    private int hpRegen;
    private float hpRegenTime;
    private float hpRegenCurrentTime;

    private int damageReduction;
    private int threshold;

    public HPStack (RGBColor color, int maxAmount = 100, int hpRegen = 0, float hpRegenTime = 0f, int damageReduction = 0, int threshold = 0) {
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

    public bool TakeDamage (RGBDamage damage, out HitStatus hitStatus) {

        damage.amount -= damageReduction;
        if (damage.color == color) {
            if (damage.amount >= threshold) {
                hitStatus = HitStatus.HIT;
                amount -= damage.amount;

                if (amount <= 0) {
                    return true;
                }
            }else { hitStatus = HitStatus.BELOW_THRESHOLD; }
        }
        else {
            if (damage.amount/3+1 >= threshold) {
                hitStatus = HitStatus.HIT;
                amount -= damage.amount/3+1;

                if (amount <= 0) {
                    return true;
                }
            }
            else { hitStatus = HitStatus.BELOW_THRESHOLD; }
        }

        return false;
    }

    public RGBColor GetColor () {
        return color;
    }

    public int GetAmount () {
        return amount;
    }

}