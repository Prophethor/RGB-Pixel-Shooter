using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UITest : MonoBehaviour {

    public Transform weapon1Panel;
    public Transform weapon2Panel;
    public Transform barrelPanel;
    public Image currentWeaponPanel;
    public Image otherWeaponPanel;

    public TestPlayer player;
    public GameManager gameManager;
    public GameObject swipeDetector;

    private bool weapon1Selected = true;
    private GameObject weapon1Barrel, weapon2Barrel;
    private Sprite weapon1Sprite, weapon2Sprite;

    private void Start () {
        gameManager.GetLoadout().GetWeapons()[0].HookUI(weapon1Panel);
        gameManager.GetLoadout().GetWeapons()[1].HookUI(weapon2Panel);
        weapon1Barrel = Instantiate(gameManager.GetLoadout().GetWeapons()[0].UIbarrel, barrelPanel);
        weapon2Barrel = Instantiate(gameManager.GetLoadout().GetWeapons()[1].UIbarrel, barrelPanel);
        weapon1Sprite = gameManager.GetLoadout().GetWeapons()[0].weaponSprite;
        weapon2Sprite = gameManager.GetLoadout().GetWeapons()[1].weaponSprite;
        currentWeaponPanel.color = Color.white;
        otherWeaponPanel.color = Color.white;
        currentWeaponPanel.sprite = weapon1Sprite;
        otherWeaponPanel.sprite = weapon2Sprite;
    }


    public void SwitchWeapon () {
        
        if (weapon1Selected) {
            weapon1Panel.gameObject.SetActive(false);
            weapon2Panel.gameObject.SetActive(true);
            weapon1Barrel.gameObject.SetActive(false);
            weapon2Barrel.gameObject.SetActive(true);
            currentWeaponPanel.sprite = weapon2Sprite;
            otherWeaponPanel.sprite = weapon1Sprite;
            weapon1Selected = false;
        }
        else {
            weapon1Panel.gameObject.SetActive(true);
            weapon2Panel.gameObject.SetActive(false);
            weapon1Barrel.gameObject.SetActive(true);
            weapon2Barrel.gameObject.SetActive(false);
            currentWeaponPanel.sprite = weapon1Sprite;
            otherWeaponPanel.sprite = weapon2Sprite;
            weapon1Selected = true;
        }
        gameManager.SwitchWeapon();
    }

    public void UnhookWeapons () {
        gameManager.GetLoadout().GetWeapons()[0].UnhookUI(weapon1Panel);
        gameManager.GetLoadout().GetWeapons()[1].UnhookUI(weapon2Panel);
    }

    private void OnDestroy () {
        //UnhookWeapons();
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