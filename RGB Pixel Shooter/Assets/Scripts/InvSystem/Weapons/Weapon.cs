using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponState { READY, COOLDOWN, LOADING }

[Serializable]
public abstract class Weapon : ScriptableObject, Item {

    protected bool isReloading = false;

    protected Dictionary<string, UnityEngine.Events.UnityAction> UIHooks;

    public Vector2 deltaPosition;
    public AnimatorOverrideController controller;
    public GameObject UIbarrel;
    public Sprite weaponSprite;

    public abstract string GetName ();

    public abstract void Shoot (Vector3 position);

    public abstract void LevelStart ();

    protected abstract void InitHooks ();

    public virtual bool CanMove () {
        return isReloading;
    }

    public virtual void HookUI (Transform weaponUI) {
        if (UIHooks == null) {
            InitHooks();
        }

        foreach (Button button in weaponUI.GetComponentsInChildren<Button>()) {
            if (UIHooks.ContainsKey(button.tag)) {
                button.onClick.AddListener(UIHooks[button.tag]);
            }
        }
    }

    public virtual void UnhookUI (Transform weaponUI) {
        foreach (Button button in weaponUI.GetComponentsInChildren<Button>()) {
            if (UIHooks.ContainsKey(button.tag)) {
                button.onClick.RemoveListener(UIHooks[button.tag]);
            }
        }
    }

}
