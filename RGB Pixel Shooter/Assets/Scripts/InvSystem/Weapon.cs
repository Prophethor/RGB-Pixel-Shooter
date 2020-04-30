using System;

[Serializable]
public abstract class Weapon : Item {

    public abstract string GetName ();

    public abstract void Shoot (int lane);
}
