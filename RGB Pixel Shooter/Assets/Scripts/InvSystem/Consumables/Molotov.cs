using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Molotov", menuName = "Consumables/Molotov")]
public class Molotov : Consumable {

    public Sprite icon;
    public float radius = 2f;
    public float duration = 5f;
    public int dps = 200;

    public GameObject explosionPrefab;
    public GameObject firePrefab;

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
        GameObject trail = Instantiate(explosionPrefab, (Vector3) position + new Vector3(0f, 0f, 1f), Quaternion.identity);
        float trailScale = 0.8f * radius;
        trail.transform.localScale = new Vector3(trailScale, trailScale, 1f);
        Tweener.Invoke(0.3f, () => Destroy(trail));

        List<GameObject> fireObjects = new List<GameObject>();
        int fireCount = Random.Range(5, 9);

        for (int i = 0; i < fireCount; i++) {
            float angle = Random.Range(0f, 2f * Mathf.PI);
            float distance = Random.Range(0f, radius);
            Vector3 firePosition = new Vector3(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance - 0.5f, Mathf.Sin(angle) * distance - 0.6f);

            GameObject fire = Instantiate(firePrefab, (Vector3) position + firePosition, Quaternion.identity);
            float scale = Random.Range(1f, 2.5f);

            fire.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
            fireObjects.Add(fire);
            fire.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            Tweener.AddTween(() => fire.transform.localScale.x, (x) => {
                fire.transform.localScale = new Vector3(x, x, 1f);
            }, scale, 0.15f, TweenMethods.Kick, () => {
                Tweener.AddTween(() => fire.transform.localScale.x, (x) => {
                    fire.transform.localScale = new Vector3(x, x, 1f);
                }, 0.01f, duration, TweenMethods.Quadratic, () => {
                    Destroy(fire);
                });
            });
        }


        AudioManager.GetInstance().PlaySound(ExplosionSFX, true);

        float temp = 0f;
        Tweener.AddTween(() => temp, (x) => {
            temp = x;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, LayerMask.GetMask("Enemies"));
            foreach (Collider2D collider in colliders) {
                Debug.Log("Molotov used");
                collider.GetComponent<GenericEnemy>().TakeDamage(new RGBDamage(RGBColor.NONE, Mathf.FloorToInt(dps * Time.deltaTime)));
            }
        }, 1f, duration);
    }

    public override ItemToken GetToken () {
        throw new System.NotImplementedException();
    }
}
