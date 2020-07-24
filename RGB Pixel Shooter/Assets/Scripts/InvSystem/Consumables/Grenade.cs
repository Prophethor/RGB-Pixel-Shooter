using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Grenade", menuName = "Consumables/Grenade")]
public class Grenade : Consumable {

    public Sprite icon;
    public float radius = 1f;
    public int damage = 400;
    public GameObject trailPrefab;

    [Header("Audio Clips")]
    public AudioClip ExplosionSFX;
    public AudioClip pickSFX;

    public override AudioClip GetPickupAudio () {
        return pickSFX;
    }

    public override string GetName () {
        return "Grenade";
    }

    public override Sprite GetIcon () {
        return icon;
    }

    public override void Use (Vector2 position) {
        GameObject trail = Instantiate(trailPrefab, (Vector3)position + new Vector3(0, 0, 1f), Quaternion.identity);
        Tweener.Invoke(0.3f, () => Destroy(trail));

        AudioManager.GetInstance().PlaySound(ExplosionSFX, true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, LayerMask.GetMask("Enemies"));

        foreach (Collider2D collider in colliders) {
            collider.GetComponent<GenericEnemy>().TakeDamage(new RGBDamage(RGBColor.NONE, damage));
        }
    }

    public override ItemToken GetToken () {
        throw new System.NotImplementedException();
    }
}
