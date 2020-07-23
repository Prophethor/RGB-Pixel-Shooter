using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Grenade", menuName = "Consumables/Grenade")]
public class Grenade : Consumable {

    public Sprite icon;
    public float radius = 1f;
    public int damage = 400;
    public GameObject trail;

    public override string GetName () {
        return "Grenade";
    }

    public override Sprite GetIcon () {
        return icon;
    }

    public override void Use (Vector2 position) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, LayerMask.GetMask("Enemies"));
        foreach (Collider2D collider in colliders) {
            collider.GetComponent<GenericEnemy>().TakeDamage(new RGBDamage(RGBColor.NONE, damage));
        }
    }

    public override ItemToken GetToken () {
        throw new System.NotImplementedException();
    }
}
