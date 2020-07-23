using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBShotgunBarrel : MonoBehaviour
{
    private TestShotgun shotgun;
    Image[] slots;
    int shootIndex = 0;
    int loadIndex = 0;

    void Start () {
        shotgun = FindObjectOfType<TestShotgun>();
        slots = GetComponentsInChildren<Image>();
        Reload();
    }

    public void Reload () {
        loadIndex = 0; shootIndex = 0;
        foreach (Image slot in slots) {
            Tweener.AddTween(() => slot.color.a, (x) => { slot.color = new Color(1, 1, 1, x); }, 1, 0.1f, true);
        }
    }

    public void LoadRed () {
        Tweener.AddTween(() => slots[loadIndex + 1].color.g, (x) => { slots[loadIndex + 1].color = new Color(1, x, x, 1); }, 0, 0.1f, true);
        loadIndex = (loadIndex + 1) % 2;
    }
    public void LoadGreen () {
        Tweener.AddTween(() => slots[loadIndex + 1].color.r, (x) => { slots[loadIndex + 1].color = new Color(x, 1, x, 1); }, 0, 0.1f, true);
        loadIndex = (loadIndex + 1) % 2;
    }

    public void LoadBlue () {
        Tweener.AddTween(() => slots[loadIndex + 1].color.r, (x) => { slots[loadIndex + 1].color = new Color(x, x, 1, 1); }, 0, 0.1f, true);
        loadIndex = (loadIndex + 1) % 2;
    }

    public void Shoot () {
        List<RGBColor> bullets = shotgun.GetBullets();
        Tweener.AddTween(() => slots[shootIndex + 1].color.a, (x) => { slots[shootIndex + 1].color = new Color(1, 1, 1, x); }, 0, 0.1f, true);
        shootIndex = (shootIndex + 1) % 2;
        if (loadIndex < shootIndex || shootIndex == 0) loadIndex = (loadIndex + 1) % 2;
    }
}
