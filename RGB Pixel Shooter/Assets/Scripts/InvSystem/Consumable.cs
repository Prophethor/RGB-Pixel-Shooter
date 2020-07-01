using System;
using UnityEngine;

[Serializable]
public abstract class Consumable : Item {

    public abstract string GetName ();

    public abstract void Use (Vector2 position);
}