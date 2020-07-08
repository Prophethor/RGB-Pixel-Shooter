using UnityEngine;
using System.Collections;

[System.Serializable]
public class RevolverToken : ItemToken {

    private int dmgAmount = 100;
    private float reloadTime = 3;

    private float bulletSpeed = 15;

    public RevolverToken (ScriptableObject obj) {
        Read(obj);
    }

    public void Read (ScriptableObject obj) {
        TestRevolver revolver = (TestRevolver) obj;

        dmgAmount = revolver.dmgAmount;
        reloadTime = revolver.reloadTime;
        bulletSpeed = revolver.bulletSpeed;
    }

    public ScriptableObject Instantiate () {
        TestRevolver revolver = ScriptableObject.Instantiate(Resources.Load<TestRevolver>("TestRevolver 1"));

        revolver.dmgAmount = dmgAmount;
        revolver.reloadTime = reloadTime;
        revolver.bulletSpeed = bulletSpeed;

        return revolver;
    }

    public string GetName () {
        return "Revolver";
    }

    public Sprite GetIcon () {
        return Resources.Load<TestRevolver>("TestRevolver 1").GetIcon();
    }
}
