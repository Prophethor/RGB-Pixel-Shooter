using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RevolverToken : ItemToken {

    private int dmgAmount = 100;
    private float reloadTime = 3;

    private float bulletSpeed = 15;

    public RevolverToken (ScriptableObject obj) {
        tags = new List<string>() {
            "Weapon"
        };

        Read(obj);
    }

    public override void Read (ScriptableObject obj) {
        Revolver revolver = (Revolver) obj;

        dmgAmount = revolver.dmgAmount;
        reloadTime = revolver.reloadTime;
        bulletSpeed = revolver.bulletSpeed;
    }

    public override ScriptableObject Instantiate () {
        Revolver revolver = ScriptableObject.Instantiate(Resources.Load<Revolver>("Data/Weapons/Revolver"));

        revolver.dmgAmount = dmgAmount;
        revolver.reloadTime = reloadTime;
        revolver.bulletSpeed = bulletSpeed;

        return revolver;
    }

    public override string GetName () {
        return "Revolver";
    }

    public override Sprite GetIcon () {
        return Resources.Load<Revolver>("Data/Weapons/Revolver").GetIcon();
    }
}
