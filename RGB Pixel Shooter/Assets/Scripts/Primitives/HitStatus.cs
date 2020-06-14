public enum HitColor {
    WRONG, CORRECT, NEUTRAL
}

public class HitStatus {
    // HIT_WRONG_COLOR, BELOW_THRESHOLD, HIT_CORRECT, HIT_NEUTRAL, INSUFFICIENT_DAMAGE

    public HitColor hitColor;
    public bool belowThreshold;
    public float damageAmount;

    public HitStatus () { }

    public HitStatus (HitColor hitColor, bool belowThreshold, float damageAmount) {
        this.hitColor = hitColor;
        this.belowThreshold = belowThreshold;
        this.damageAmount = damageAmount;
    }
}