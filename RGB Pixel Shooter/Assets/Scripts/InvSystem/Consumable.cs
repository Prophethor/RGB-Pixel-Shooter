using System;

[Serializable]
public abstract class Consumable : Item {
    public abstract string GetName ();

    // TODO: Use() ?
}