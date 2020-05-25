using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Revolver", menuName = "Revolver")]
public class Revolver : Weapon {
    public int dmgAmount = 1;
    public Rigidbody2D bulletPrefab;
    List<RGBColor> bullets;
    public float bulletSpeed = 5;

    private static Dictionary<string, UnityEngine.Events.UnityAction> UIHooks;

    public override void LevelStart () {
        bullets = new List<RGBColor>();

        if (UIHooks == null) {
            InitHooks();
        }
    }

    public override string GetName () { return "Revolver"; }

    private void InitHooks () {
        UIHooks = new Dictionary<string, UnityEngine.Events.UnityAction>();
        UIHooks.Add("LoadRed", () => Load(RGBColor.RED));
        UIHooks.Add("LoadGreen", () => Load(RGBColor.GREEN));
        UIHooks.Add("LoadBlue", () => Load(RGBColor.BLUE));
        UIHooks.Add("Shoot", () => Shoot(GetPlayerPos()));
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public override void HookUI (Transform parentPanel) {
        if (UIHooks == null) {
            InitHooks();
        }

        for (int i = 0; i < parentPanel.transform.childCount; i++) {
            if (UIHooks.ContainsKey(parentPanel.GetChild(i).tag)) {
                parentPanel.GetChild(i).GetComponent<Button>().onClick.AddListener(UIHooks[parentPanel.GetChild(i).tag]);
            }
        }
    }

    public void UnookUI (GameObject parentPanel) {
        for (int i = 0; i < parentPanel.transform.childCount; i++) {
            if (UIHooks.ContainsKey(parentPanel.transform.GetChild(i).tag)) {
                parentPanel.transform.GetChild(i).GetComponent<Button>().onClick.RemoveListener(UIHooks[parentPanel.transform.GetChild(i).tag]);
            }
        }
    }

    public void Load (RGBColor color) {
        if (bullets.Count < 6) {
            bullets.Add(color);
        }
    }

    public override void Shoot (Vector3 position) {
        if (bullets.Count > 0) {
            Rigidbody2D bulletObj = Instantiate(bulletPrefab, (Vector3) deltaPosition + position, Quaternion.identity);
            bulletObj.velocity = new Vector2(bulletSpeed, 0);
            bulletObj.GetComponent<Projectile>().SetDamage(bullets[0], dmgAmount);
            bullets.RemoveAt(0);
        }
    }
}
