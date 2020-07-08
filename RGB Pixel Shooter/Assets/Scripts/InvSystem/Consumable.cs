using System;
using UnityEngine;

[Serializable]
public abstract class Consumable : Item {

    public abstract string GetName ();

    public Sprite GetIcon () {
        // TODO: Consumable icons
        return null;
    }

    public abstract ItemToken GetToken ();

    public abstract void Use (Vector2 position);
}