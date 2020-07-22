using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeToken : ItemToken {

    // TODO: Stack size
    private float radius;
    private int damage;

    public GrenadeToken (ScriptableObject obj) {
        tags = new List<string>() {
            "Consumable"
        };

        Read(obj);
    }

    public override void Read (ScriptableObject obj) {
        Grenade grenade = (Grenade) obj;

        radius = grenade.radius;
        damage = grenade.damage;
    }

    public override ScriptableObject Instantiate () {
        Grenade grenade = ScriptableObject.Instantiate(Resources.Load<Grenade>("Data/Weapons/TestRevolver"));

        grenade.radius = radius;
        grenade.damage = damage;

        return grenade;
    }

    public override Sprite GetIcon () {
        return Resources.Load<Grenade>("Data/Weapons/Grenade").GetIcon();
    }

    public override string GetName () {
        return "Grenade";
    }
}
