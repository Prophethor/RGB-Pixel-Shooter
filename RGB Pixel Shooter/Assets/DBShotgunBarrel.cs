using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBShotgunBarrel : MonoBehaviour
{
    private TestShotgun shotgun;
    Image[] slots;

    void Start () {
        shotgun = FindObjectOfType<TestShotgun>();
        slots = GetComponentsInChildren<Image>();
    }

    private void Update () {
        List<RGBColor> bullets = shotgun.GetBullets();
        for (int i = 1; i <= bullets.Count; i++) {
            slots[i].color = bullets[i - 1].GetColor();
        }
        for (int i = bullets.Count + 1; i <= 2; i++) {
            slots[i].color = new Color(0, 0, 0, 0);
        }
    }
}
