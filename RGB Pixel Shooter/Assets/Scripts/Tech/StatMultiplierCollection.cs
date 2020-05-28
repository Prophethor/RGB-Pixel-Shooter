using System.Collections.Generic;

public class StatMultiplierCollection {

    private Dictionary<StatEnum, float> statMultipliers;

    public StatMultiplierCollection () {
        statMultipliers = new Dictionary<StatEnum, float>();
    }

    public float GetStat (StatEnum statIndex) {
        if (!statMultipliers.ContainsKey(statIndex)) {
            statMultipliers[statIndex] = 1f;
        }
        return statMultipliers[statIndex];
    }

    /*  With this implementations, multipliers stack additively
        That means, if you add two multipliers which both increase, let's say, speed by 50%
        you will end up with a speed multiplier of 200%, or 2 */
    public void AddMultiplier (StatEnum statIndex, float value) {
        if (!statMultipliers.ContainsKey(statIndex)) {
            statMultipliers[statIndex] = 1f;
        }

        statMultipliers[statIndex] += value;
    }
}
