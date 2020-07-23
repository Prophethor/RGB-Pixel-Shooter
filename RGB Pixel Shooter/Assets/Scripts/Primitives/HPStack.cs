using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

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

    private Callback onDestroy;

    private GameObject HP;

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

    public void SetOnDestroy(Callback callback)
    {
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

        damage.amount -= damageReduction;
        if (damage.color == color) {
            if (damage.amount >= threshold) {
                hitStatus = HitStatus.HIT;
                amount -= damage.amount;
                if (amount < 0) amount = 0;

                HP.transform.localScale = new Vector3(2.8f * (float) amount / maxAmount, HP.transform.localScale.y);

                if (amount == 0 && onDestroy != null) {
                    onDestroy();
                    return true;

                }
                else if(amount == 0)
                {
                    return true;
                }
            }
            else { hitStatus = HitStatus.BELOW_THRESHOLD; }
        }
        else {
            if (damage.amount / 4 + 1 >= threshold) {
                hitStatus = HitStatus.HIT;
                amount -= damage.amount / 4 + 1;
                if (amount < 0) amount = 0;
                HP.transform.localScale = new Vector3(2.8f * (float) amount / maxAmount, HP.transform.localScale.y);

                if (amount == 0 && onDestroy != null)
                {
                    onDestroy();
                    return true;

                }
                else if (amount == 0)
                {
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

    public void SetHPBar (GameObject HP) {
        this.HP = HP;
        this.HP.GetComponent<SpriteRenderer>().color = color.GetColor();
    }
}