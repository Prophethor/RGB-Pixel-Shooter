using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Weapon : ItemToken {

    protected WeaponInfo weaponInfo;

    protected Sprite icon;
    protected string name;

    protected bool isReloading = false;

    protected Dictionary<string, UnityEngine.Events.UnityAction> UIHooks;

    public virtual AnimatorOverrideController Controller {
        get {
            if (weaponInfo == null) {
                LoadWeaponInfo();
            }
            return weaponInfo.controller;
        }
    }

    public virtual GameObject UIBarrel {
        get {
            if (weaponInfo == null) {
                LoadWeaponInfo();
            }
            return weaponInfo.UIbarrel;
        }
    }

    public virtual Sprite WeaponSprite {
        get {
            if (weaponInfo == null) {
                LoadWeaponInfo();
            }
            return weaponInfo.weaponSprite;
        }
    }

    protected abstract void LoadWeaponInfo ();

    public override Sprite GetIcon () {
        if (icon == null) {
            LoadWeaponInfo();
        }

        return icon;
    }

    public override string GetName () {
        if (name == null || name == "") {
            LoadWeaponInfo();
        }

        return name;
    }

    public abstract void LevelStart ();

    protected abstract void InitHooks ();

    public virtual bool CanMove () {
        return isReloading;
    }

    public abstract void Shoot ();

    public abstract void Load (RGBColor color);

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

    public override ScriptableObject Instantiate () {
        throw new System.NotImplementedException();
    }

    public override void Read (ScriptableObject obj) {
        throw new System.NotImplementedException();
    }
}
