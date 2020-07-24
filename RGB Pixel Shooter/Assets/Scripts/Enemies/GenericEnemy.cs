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

    protected GameManager gm;
    protected bool isDead = false;
    protected SpriteRenderer sr;
    protected Material defaultMaterial;
    protected Material flashMaterial;

    protected virtual void Awake () {
        Initialize();
    }

    protected virtual void Start () {
        // Set position according to lane
        float yPos = PlayField.GetLanePosition(lane) - Random.Range(-0.33f, 1f) * PlayField.GetLaneHeight() / 3f;
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x + 2, yPos, yPos);

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        animator.SetTrigger("is" + baseColor.GetString());
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
        sr = GetComponent<SpriteRenderer>();
        flashMaterial = Resources.Load("TestMat", typeof(Material)) as Material;
        defaultMaterial = sr.material;
        animator = GetComponent<Animator>();
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

    public virtual void Flash (float duration) {
        sr.material = flashMaterial;
        Tweener.Invoke(duration, () => sr.material = defaultMaterial);
    }

    public virtual void UpdateMove()
    {
        Move();
    }
    public virtual void TakeDamage (RGBDamage damage) {
        if (hpStackList.Count == 0) {
            return;
        }

        Flash(0.1f);

        HitStatus hitStatus;

        if (hpStackList[0].TakeDamage(damage, out hitStatus)) {
            hpStackList.RemoveAt(0);

            if (hpStackList.Count == 0) {
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false;
                animator.SetBool("isDead", isDead);
                animator.SetTrigger("is" + baseColor.GetString());
                Tweener.Invoke(.1f, () => sr.color = Color.gray);
                Move();
                Die();
            }
        }
    }

    // ¯\_(ツ)_/¯
    public virtual void SetAnimatorSpeed (float speed) {
        animator.speed = speed;
    }

    protected virtual void Die () {
        EnemySpawner.enemiesToKill--;
        SelfDestruct();
    }

    public virtual void SelfDestruct () {
        Destroy(gameObject, 1f);
    }

    public void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullets")) {
            //ova linija ispod mora da se desi pre take damage linije
            if (this.hpStackList.Count > 0) {
                collision.gameObject.GetComponent<Projectile>().SpawnBloodSplater(collision.transform.position, collision.gameObject.GetComponent<Projectile>().damage.color, this.hpStackList[0].GetColor());
            }
            else {
                collision.gameObject.GetComponent<Projectile>().SpawnBloodSplater(collision.transform.position, collision.gameObject.GetComponent<Projectile>().damage.color);
            }

            TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            // metak stvara blood splatter, mozda moze pametnije da se odradi pa da bude drugacijij blood splater svaki put
            collision.gameObject.GetComponent<Projectile>().SelfDestruct();
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
