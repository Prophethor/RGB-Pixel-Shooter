using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class GenericEnemy : MonoBehaviour {

    protected Rigidbody2D rb;
    protected Animator animator;
    protected RGBColor baseColor;

    protected float speed;
    protected int lane;

    protected List<HPStack> hpStackList;
    protected List<Trait> traits;

    protected virtual void Awake () {
        Initialize();
    }

    protected virtual void Start () {
        // Set position according to lane
        transform.position = new Vector3(8, PlayField.GetLanePosition(lane));

        animator = GetComponent<Animator>();
        switch (baseColor) {
            case RGBColor.RED:
                animator.SetTrigger("isRed");
                break;
            case RGBColor.GREEN:
                animator.SetTrigger("isGreen");
                break;
            case RGBColor.BLUE:
                animator.SetTrigger("isBlue");
                break;
        }
        rb = GetComponent<Rigidbody2D>();
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

    public int GetLane () {
        return lane;
    }

    public void SetLane (int lane) {
        this.lane = lane;
    }

    public RGBColor GetBaseColor () {
        return baseColor;
    }

    public void SetBaseColor (RGBColor baseColor) {
        this.baseColor = baseColor;
    }

    public void SetSpeed (float speed) {
        this.speed = speed;
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
