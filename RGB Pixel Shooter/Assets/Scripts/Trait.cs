using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trait {

    public GenericEnemy enemy;

    // Called when the trait is added to the enemy
    public virtual void Start () { }

    public virtual void Update (float deltaTime) { }

    public virtual void Die () { }

    // Traits rely on enemies setting themselves using this method, before using any other trait methods
    public void SetEnemy (GenericEnemy enemy) {
        this.enemy = enemy;
    }
}
