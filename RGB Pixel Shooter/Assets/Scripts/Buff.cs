using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff {

    public Dictionary<StatEnum, float> statMultipliers;
    public float buffDuration;

    // The default value of 0f means that, by default, buffs last forever
    public Buff (Dictionary<StatEnum, float> statMultipliers, float buffDuration = 0f) {
        this.statMultipliers = statMultipliers;
        this.buffDuration = buffDuration;
    }

    public void Apply (StatMultiplierCollection smc) {
        foreach (StatEnum key in statMultipliers.Keys) {
            smc.AddMultiplier(key, statMultipliers[key]);

            if (buffDuration > float.Epsilon) {
                Tweener.Invoke(buffDuration, () => {
                    smc.AddMultiplier(key, -statMultipliers[key]);
                });
            }
        }
    }
}
