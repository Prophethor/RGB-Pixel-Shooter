using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour {

    public Transform weapon1Panel;
    public Transform weapon2Panel;

    public TestPlayer player;
    public RectTransform weaponPanel;
    private bool weapon1Selected = true;

    private void Start () {
        ((Revolver) player.equippedWeapon).HookUI(weapon1Panel);
        Swipe.OnSwipe += debSwip;
    }


    public void debSwip (Swipe.SwipeData swipe) {
        if (posOnPanel(swipe.startPos, weaponPanel) && (swipe.direction == Swipe.SwipeDirection.Up || swipe.direction == Swipe.SwipeDirection.Down)) {
            if (weapon1Selected) {
                weapon1Panel.gameObject.SetActive(false);
                weapon2Panel.gameObject.SetActive(true);
                weapon1Selected = false;
            }
            else {
                weapon1Panel.gameObject.SetActive(true);
                weapon2Panel.gameObject.SetActive(false);
                weapon1Selected = true;
            }
            Weapon tempWeap = player.equippedWeapon;
            player.equippedWeapon = player.otherWeapon;
            player.otherWeapon = tempWeap;
        }
    }
    bool posOnPanel (Vector2 touch, RectTransform panel) {
        if ((touch.x <= panel.position.x && touch.x >= panel.position.x - panel.rect.width) /*&& 
            (touch.y >= panel.position.y && touch.y <= panel.position.y + panel.rect.height)*/) return true;
        return false;
    }

    public void ButtonLog (string msg) {
        Debug.Log(msg);
    }
}
