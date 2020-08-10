using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrait : Trait {

    private RGBColor color;
    private int amount;

    public ShieldTrait (RGBColor color, int amount) {
        this.color = color;
        this.amount = amount;
    }

    public override void Start () {
        enemy.AddHPStack(new HPStack(color, amount, 0, 0f, 0, amount));
    }
}
