using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FreezeBomb", menuName = "Consumables/FreezeBomb")]
public class FreezeBomb : Consumable {

    public Sprite icon;
    public float radius = 1f;
    public float thawTime = 1f;
    private const float slowFactor = -1f;
    public GameObject trailPrefab;
    public GameObject iciclePrefab;

    [Header("Audio Clips")]
    public AudioClip ExplosionSFX;
    public AudioClip pickSFX;

    public override AudioClip GetPickupAudio () {
        return pickSFX;
    }

    public override string GetName () {
        return "FreezeBomb";
    }

    public override Sprite GetIcon () {
        return icon;
    }

    public override void Use (Vector2 position) {
        GameObject trail = Instantiate(trailPrefab, (Vector3) position + new Vector3(0, 0, 1f), Quaternion.identity);
        Tweener.Invoke(0.3f, () => Destroy(trail));

        AudioManager.GetInstance().PlaySound(ExplosionSFX, true);

        Buff freeze = new Buff(new Dictionary<StatEnum, float>() { { StatEnum.SPEED, slowFactor } }, thawTime);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, LayerMask.GetMask("Enemies"));
        foreach (Collider2D collider in colliders) {
            collider.GetComponent<GenericEnemy>().ApplyBuff(freeze);
            collider.GetComponent<GenericEnemy>().SetAnimatorSpeed(0f);
            collider.GetComponent<GenericEnemy>().Flash(thawTime);
            collider.GetComponent<SpriteRenderer>().color = new Color(0f, 0.9f, 0.87f);

            Vector3 enemyPos = collider.transform.position;

            GameObject icicle = Instantiate(iciclePrefab, new Vector3(enemyPos.x, enemyPos.y, enemyPos.y - 0.01f), Quaternion.identity);
            Tweener.AddTween(() => icicle.transform.localScale.x, (x) => {
                icicle.transform.localScale = new Vector3(x, x, 1f);
            }, 0.01f, thawTime, TweenMethods.Linear, () => {
                collider.GetComponent<GenericEnemy>().SetAnimatorSpeed(1f);
                collider.GetComponent<SpriteRenderer>().color = Color.white;
                Destroy(icicle);
            });
        }

        // Leave code for molotov
        //float temp = 0f;
        //Tweener.AddTween(() => temp, (x) => {
        //    temp = x;
        //    Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, LayerMask.GetMask("Enemies"));
        //    foreach (Collider2D collider in colliders) {
        //        Debug.Log("Freeze used");
        //        collider.GetComponent<GenericEnemy>().ApplyBuff(new Buff(new Dictionary<StatEnum, float>() { { StatEnum.SPEED, slowFactor } }, thawTime));
        //    }
        //}, 1f, duration);
    }

    public override ItemToken GetToken () {
        throw new System.NotImplementedException();
    }
}
