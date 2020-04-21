using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEnemy : MonoBehaviour {

    public int point;

    protected float speed;
    protected int lane;

    protected List<HPStack> hpStackList;
    protected List<Trait> traits;

    protected virtual void Awake () {
        Initialize();
    }

    protected virtual void Update () {
        foreach (Trait trait in traits) {
            trait.Update(Time.deltaTime);
        }
        foreach (HPStack stack in hpStackList) {
            stack.Update(Time.deltaTime);
        }
    }

    protected void Initialize () {
        hpStackList = new List<HPStack>();
        traits = new List<Trait>();
    }

    public virtual void AddTrait (Trait trait) {
        traits.Add(trait);
        trait.SetEnemy(this);
        trait.Start();
    }

    public virtual void AddHPStack (HPStack stack) {
        hpStackList.Add(stack);
    }

    protected abstract void Move ();

    public virtual void TakeDamage (RGBDamage damage) {
        HitStatus hitStatus;

        if (hpStackList[0].TakeDamage(damage, out hitStatus)) {
            hpStackList.RemoveAt(0);

            if (hpStackList.Count == 0) {
                Die();
            }
        }
    }

    protected virtual void Die () {
        SelfDestruct();
    }

    public virtual void SelfDestruct () {
        Destroy(gameObject);
    }

    protected abstract void InitiateShanking ();
}
