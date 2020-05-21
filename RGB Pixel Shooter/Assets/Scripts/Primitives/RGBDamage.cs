using UnityEngine;

[System.Serializable]
public struct RGBDamage {
    public RGBColor color;
    public int amount;

    public RGBDamage (RGBColor color, int amount) {
        this.color = color;
        this.amount = amount;
    }
}
