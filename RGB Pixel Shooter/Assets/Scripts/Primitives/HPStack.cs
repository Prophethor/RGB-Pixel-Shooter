public class HPStack {
    private RGBColor color;
    private int amount;

    private int hpRegen;
    private float hpRegenTime;

    private int damageReduction;
    private int threshold;

    public HPStack (RGBColor color, int amount = 1, int hpRegen = 0, float hpRegenTime = 0f, int damageReduction = 0, int threshold = 0) {
        this.color = color;
        this.amount = amount;

        this.hpRegen = hpRegen;
        this.hpRegenTime = hpRegenTime;

        this.damageReduction = damageReduction;
        this.threshold = threshold;
    }

    public void Update (float deltaTime) {
        // TODO: The Enemy calls the HPStack.Update method from its own Update method
    }

    public void TakeDamage (RGBDamage damage) {
        // TODO: Implement TakeDamage method
    }
}