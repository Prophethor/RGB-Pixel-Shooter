using System;

[Serializable]
public abstract class Gear : Item {
    public abstract string GetName ();

    // TODO: ApplyEffects() ?
}
