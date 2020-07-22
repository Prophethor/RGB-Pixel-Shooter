using System;
using UnityEngine;

[Serializable]
public abstract class Consumable : ScriptableObject, Item {

    public abstract string GetName ();

    public virtual Sprite GetIcon () {
        // TODO: Consumable icons
        return null;
    }

    public abstract ItemToken GetToken ();

    public abstract void Use (Vector2 position);
}