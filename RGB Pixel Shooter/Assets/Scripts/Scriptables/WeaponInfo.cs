using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponState { READY, COOLDOWN, LOADING }

[Serializable]
public abstract class WeaponInfo : ScriptableObject, Item {

    public Vector2 deltaPosition;
    public AnimatorOverrideController controller;
    public GameObject UIbarrel;
    public Sprite weaponSprite;

    public abstract string GetName ();

    public Sprite GetIcon () {
        return weaponSprite;
    }
}
