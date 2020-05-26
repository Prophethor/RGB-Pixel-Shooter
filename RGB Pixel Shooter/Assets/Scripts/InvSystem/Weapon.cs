using System;
using UnityEngine;

[Serializable]
public abstract class Weapon : ScriptableObject, Item {

    public Vector2 deltaPosition;

    public abstract string GetName ();

    public abstract void Shoot (Vector3 position);

    public abstract void LevelStart ();

    public abstract void HookUI (Transform weaponPanel);
}
