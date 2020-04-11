using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEnemy : MonoBehaviour {
    public int point;

    protected float speed;
    protected int lane;

    protected List<HPStack> hpStackList;

    protected abstract void Move ();

    public abstract void TakeDamage (RGBDamage damage);

    protected abstract void Die ();

    protected abstract void InitiateShanking ();

    public abstract void SelfDestruct ();
}
