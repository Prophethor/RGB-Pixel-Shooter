using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class GenericEnemy : MonoBehaviour, Statable {

    protected Rigidbody2D rb;
    protected Animator animator;
    protected RGBColor baseColor;

    protected StatMultiplierCollection statMultipliers;

    protected float speed;
    protected int lane;

    protected List<HPStack> hpStackList;
    protected List<Trait> traits;

    private GameManager gm;
    protected bool isDead = false;

    protected virtual void Awake () {
        Initialize();
    }

    protected virtual void Start () {
        // Set position according to lane
        float yPos = PlayField.GetLanePosition(lane) - Random.Range(-0.33f, 1f) * PlayField.GetLaneHeight() / 3f;
        transform.position = new Vector3(9, yPos, yPos);

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        animator.SetBool("isDead", isDead);// false po defaultu
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update () {
        foreach (Trait trait in traits) {
            trait.Update(Time.deltaTime);
        }
        foreach (HPStack stack in hpStackList) {
            stack.Update(Time.deltaTime);
        }

        // Calling Move every frame might not be necessary, but it also might be
        //Move();
    }

    protected void Initialize () {
        hpStackList = new List<HPStack>();
        statMultipliers = new StatMultiplierCollection();
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

    public StatMultiplierCollection GetStatMultipliers () {
        return statMultipliers;
    }

    public void ApplyBuff (Buff buff) {
        buff.Apply(statMultipliers);
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
                isDead = true;
                animator.SetBool("isDead", isDead);
                switch (baseColor)
                {
                    case RGBColor.RED:
                        animator.SetTrigger("isRed");
                        break;
                    case RGBColor.GREEN:
                        animator.SetTrigger("isGreen");
                        break;
                    case RGBColor.BLUE:
                        animator.SetTrigger("isBlue");
                        // test code, delete after adding anim
                        break;
                }
                Move();
                Die();
            }
        }
    }

    protected virtual void Die () {
        EnemySpawner.enemiesToKill--;
        SelfDestruct();
    }

    public virtual void SelfDestruct() {
        //Destroy(gameObject, 2f);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullets")) {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            collision.gameObject.GetComponent<Projectile>().SpawnBloodSplater(collision.transform.position); // metak stvara blood splatter
            Destroy(collision.gameObject);
        }
    }

    public void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("FinishLine")) {
            gm.LoseGame();
        }
    }

    protected abstract void InitiateShanking ();
}
