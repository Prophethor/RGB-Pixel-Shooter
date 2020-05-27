using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UITest : MonoBehaviour {

    public Transform weapon1Panel;
    public Transform weapon2Panel;

    public GameManager gameManager;

    public TestPlayer player;
    public RectTransform weaponPanel;
    private bool weapon1Selected = true;

    private void Start () {
        gameManager.GetLoadout().GetWeapons()[0].HookUI(weapon1Panel);
        gameManager.GetLoadout().GetWeapons()[1].HookUI(weapon2Panel);
        Swipe.OnSwipe += debSwip;
    }


    public void debSwip (Swipe.SwipeData swipe) {
        if (posOnPanel(swipe.startPos, weaponPanel) && (swipe.direction == Swipe.SwipeDirection.Up || swipe.direction == Swipe.SwipeDirection.Down)) {
            SwitchWeapon();
        }
    }

    public void SwitchWeapon () {
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
        gameManager.SwitchWeapon();
    }

    private bool posOnPanel (Vector2 touch, RectTransform panel) {
        if (touch.x <= panel.position.x && touch.x >= panel.position.x - panel.rect.width) {
            return true;
        }
        return false;
    }

    public void ButtonLog (string msg) {
        Debug.Log(msg);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(UITest))]
public class UITestEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (GUILayout.Button("Switch weapon")) {
            ((UITest) target).SwitchWeapon();
        }
    }
}

#endif