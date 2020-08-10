using System;
using UnityEngine;

[Serializable]
public abstract class Gear : Item {
    public abstract string GetName ();

    public Sprite GetIcon () {
        // TODO: Gear icon
        return null;
    }

    public abstract ItemToken GetToken ();

    // TODO: ApplyEffects() ?
}
