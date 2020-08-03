using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevolverBarrel : MonoBehaviour {

    private Revolver revolver;
    Image[] slots;
    int shootIndex = 0;
    int loadIndex = 0;

    private Tween rotTween;

    void Start () {
        revolver = FindObjectOfType<Revolver>();
        slots = GetComponentsInChildren<Image>();
        Reload();
    }

    public void Reload () {
        loadIndex = 0; shootIndex = 0;
        foreach (Image slot in slots) {
            Tweener.AddTween(() => slot.color.a, (x) => { slot.color = new Color(1, 1, 1, x); }, 1, 0.1f, true);
        }
    }

    private void ReadRevolver () {
        List<RGBColor> bullets = revolver.GetBullets();
        for (int i = 0; i < bullets.Count; i++) {

        }
    }

    public void LoadRed () {
        int localIndex = loadIndex + 1;
        Tweener.AddTween(() => slots[localIndex].color.g, (x) => { slots[localIndex].color = new Color(1, x, x, 1); }, 0, 0.1f, true);
        loadIndex = (loadIndex + 1) % 6;
    }
    public void LoadGreen () {
        int localIndex = loadIndex + 1;
        Tweener.AddTween(() => slots[localIndex].color.r, (x) => { slots[localIndex].color = new Color(x, 1, x, 1); }, 0, 0.1f, true);
        loadIndex = (loadIndex + 1) % 6;
    }

    public void LoadBlue () {
        int localIndex = loadIndex + 1;
        Tweener.AddTween(() => slots[localIndex].color.r, (x) => { slots[localIndex].color = new Color(x, x, 1, 1); }, 0, 0.1f, true);
        loadIndex = (loadIndex + 1) % 6;
    }

    public void Shoot () {
        List<RGBColor> bullets = revolver.GetBullets();
        int localIndex = shootIndex + 1;
        Tweener.AddTween(() => slots[localIndex].color.a, (x) => { slots[localIndex].color = new Color(1, 1, 1, x); }, 0, 0.1f, true);
        shootIndex = (shootIndex + 1) % 6;
        if (loadIndex < shootIndex || shootIndex == 0) loadIndex = (loadIndex + 1) % 6;

        if (rotTween != null && rotTween.active) {
            rotTween.Finish();
        }
        rotTween = Tweener.AddTween(() => slots[0].transform.rotation.eulerAngles.z, (x) => { slots[0].transform.rotation = Quaternion.Euler(0, 0, x); }, slots[0].transform.rotation.eulerAngles.z - 60, 0.1f, true);
    }

}
