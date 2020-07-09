using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevolverBarrel : MonoBehaviour
{
    private TestRevolver revolver;
    Image[] slots;

    void Start()
    {
        revolver = FindObjectOfType<TestRevolver>();
        slots = GetComponentsInChildren<Image>();
    }

    private void Update () {
        List<RGBColor> bullets = revolver.GetBullets();
        for(int i=1; i<=bullets.Count; i++) {
            slots[i].color = bullets[i-1].GetColor();
        }
        for(int i=bullets.Count+1; i<=6; i++) {
            slots[i].color = new Color(0, 0, 0, 0);
        }
    }
}
