using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ShotgunToken : ItemToken {

    private int dmgAmount = 100;
    private float reloadTime = 3;

    private float bulletSpeed = 15;
    private float range;
    private double angle;

    public ShotgunToken (ScriptableObject obj) {
        tags = new List<string>() {
            "Weapon"
        };

        Read(obj);
    }

    public override void Read (ScriptableObject obj) {
        DBShotgun shotgun = (DBShotgun) obj;

        dmgAmount = shotgun.dmgAmount;
        reloadTime = shotgun.reloadTime;

        bulletSpeed = shotgun.bulletSpeed;
        range = shotgun.range;
        angle = shotgun.angle;
    }

    public override ScriptableObject Instantiate () {
        DBShotgun shotgun = ScriptableObject.Instantiate(Resources.Load<DBShotgun>("Data/Weapons/DBShotgun"));

        shotgun.dmgAmount = dmgAmount;
        shotgun.reloadTime = reloadTime;

        shotgun.bulletSpeed = bulletSpeed;
        shotgun.range = range;
        shotgun.angle = angle;

        return shotgun;
    }

    public override string GetName () {
        return "Shotgun";
    }

    public override Sprite GetIcon () {
        return Resources.Load<DBShotgun>("Data/Weapons/DBShotgun").GetIcon();
    }
}
