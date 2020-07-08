using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBomb : Consumable {

    public float radius;

    public override string GetName () {
        return "ColorBomb";
    }

    public override ItemToken GetToken () {
        throw new System.NotImplementedException();
    }

    // Takes world space position
    public override void Use (Vector2 position) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, LayerMask.GetMask("Enemies"));

        foreach (Collider2D collider in colliders) {
            collider.GetComponent<GenericEnemy>().TakeDamage(new RGBDamage(RGBColor.RED, 2));
            collider.GetComponent<GenericEnemy>().TakeDamage(new RGBDamage(RGBColor.GREEN, 2));
            collider.GetComponent<GenericEnemy>().TakeDamage(new RGBDamage(RGBColor.BLUE, 2));
        }
    }
}
