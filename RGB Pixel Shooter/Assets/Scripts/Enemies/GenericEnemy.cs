using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class GenericEnemy : MonoBehaviour, Statable {

    protected Rigidbody2D rb;
    protected RGBColor baseColor;

    protected StatMultiplierCollection statMultipliers;

    protected float speed;
    protected int pointValue;
    protected int lane;

    protected List<HPStack> hpStackList;
    protected List<Trait> traits;

    protected GameManager gm;
    protected ExplosionManager explosionManager;
    protected bool isDead = false;
    protected SpriteRenderer sr;
    protected Material defaultMaterial;
    protected Material flashMaterial;

    protected Animator animator;   
   
    protected virtual void Awake () {
        Initialize();

    }

    protected virtual void Start () {
        // Set position according to lane
         // TODO delete this
        float yPos = PlayField.GetLanePosition(lane) - Random.Range(-0.33f, 1f) * PlayField.GetLaneHeight() / 3f;
        transform.position = new Vector3(9, yPos, yPos);

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        flashMaterial = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        defaultMaterial = sr.material;
        animator = GetComponent<Animator>();
        explosionManager = FindObjectOfType<ExplosionManager>();


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

    public void SetPointValue(int pointValue)
    {
        this.pointValue = pointValue;
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


    public virtual HitStatus TakeDamage (RGBDamage damage) {
        HitStatus hitStatus;

        if (hpStackList[0].TakeDamage(damage, out hitStatus)) {

            if (hpStackList[0].GetAmount() > 0)
            {
                sr.material = flashMaterial;
                Invoke("ResetMaterial", .1f);
            }
            else
            {
                sr.material = flashMaterial;
                Invoke("ResetMaterial", .1f);
                hpStackList.RemoveAt(0);
            }

            if (hpStackList.Count == 0) {
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false;
                animator.SetBool("isDead", isDead);


                // Temporary; TODO: change sprites to reflect behavior below

                

                Move();
                Die();
            }
        }

        return hitStatus;
    }

    protected virtual void Die () {
        EnemySpawner.enemiesToKill--;
        gm.UpdateScore(pointValue);
        SelfDestruct();
    }

    public virtual void SelfDestruct () {
        Destroy(gameObject, 1f);
    }

    public void OnCollisionEnter2D (Collision2D collision) {
        HitStatus hitData;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullets")) {
            CameraShake.instance.Shake(0.1f);
            RGBColor enemyColor = hpStackList[0].GetColor();
            RGBColor bulletColor = collision.gameObject.GetComponent<Projectile>().damage.color;
            hitData = TakeDamage(collision.gameObject.GetComponent<Projectile>().damage); // cuvamo retutn koji je data, ali take damage obracunava svu stetu... hopefully

            explosionManager.SpawnExplosion(bulletColor, collision, hitData); // saljemo hit data iz take damage u projektil
            Destroy(collision.gameObject);
        }
    }

    public void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("FinishLine")) {
            gm.LoseGame();
        }
    }

    public virtual void FlashMaterial()
    {
        sr.material = flashMaterial;
        Invoke("ResetMaterial", .1f);
    }

    public virtual void ResetMaterial () {
        sr.material = defaultMaterial;
    }

    protected abstract void InitiateShanking ();
}
