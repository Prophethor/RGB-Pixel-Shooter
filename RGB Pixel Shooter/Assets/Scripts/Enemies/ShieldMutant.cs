using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMutant : GenericEnemy {

    public GameObject HP1;
    public GameObject HP2;

    [Header("Sound effects")]
    public List<AudioClip> spawnEffects;
    public List<AudioClip> deathEffects;
    public AudioClip shieldBreakEffect;
    public AudioClip shieldDeflectEffect;

    private float vocalPitch;

    private SpriteRenderer childSr;

    public AnimatorOverrideController OverrideRed;
    public AnimatorOverrideController OverrideBlue;
    public AnimatorOverrideController OverrideGreen;

    public AnimatorOverrideController OverrideShieldRed;
    public AnimatorOverrideController OverrideShieldBlue;
    public AnimatorOverrideController OverrideShieldGreen;

    private Animator animatorShield;

    protected override void Start () {

        float yPos = PlayField.GetLanePosition(lane) - Random.Range(-0.33f, 1f) * PlayField.GetLaneHeight() / 3f;
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x + 2, yPos, yPos);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        vocalPitch = Random.Range(0.8f, 1.5f);

        if (spawnEffects.Count > 0) {
            AudioManager.GetInstance().PlaySoundPitched(spawnEffects[Random.Range(0, spawnEffects.Count)], vocalPitch);
        }

        RGBColor randcolor = (RGBColor) Random.Range(0, 3);
        hpStackList.Add(new HPStack(randcolor, 100, 0, 0, 0, 100));
        hpStackList[0].SetHPBar(HP1);
        hpStackList.Add(new HPStack(baseColor, 100));
        hpStackList[1].SetHPBar(HP2);

        animatorShield = GetComponentsInChildren<Animator>()[1];

        hpStackList[0].SetOnDestroy(() => {
            hpStackList.RemoveAt(0);
            Tweener.Invoke(0.1f, () => sr.material = defaultMaterial);
            Tweener.Invoke(0.1f, () => childSr.material = defaultMaterial);

            // ovde puca prvi stack, ako je prosao treshold i vratio true
            animator.SetBool("hasShield", false);
            animator.SetTrigger("break");
            animatorShield.SetTrigger("break");
            AudioManager.GetInstance().PlaySoundPitched(shieldBreakEffect, 1f);
        });

        hpStackList[1].SetOnDestroy(() => {

            Tweener.Invoke(0.1f, () => sr.material = defaultMaterial);
            isDead = true;
            animator.SetBool("isDead", isDead);
            GetComponent<BoxCollider2D>().enabled = false;
            Tweener.Invoke(.1f, () => sr.color = Color.gray);
            Move();
            Die();
            hpStackList.RemoveAt(0);
        });

        switch (baseColor)
        {
            case RGBColor.RED:
                animator.runtimeAnimatorController = OverrideRed;
                break;
            case RGBColor.GREEN:
                animator.runtimeAnimatorController = OverrideGreen;
                break;
            case RGBColor.BLUE:
                animator.runtimeAnimatorController = OverrideBlue;
                break;
            case RGBColor.NONE:
                break;
            default:
                break;
        }

        switch (randcolor)
        {
            case RGBColor.RED:
                animatorShield.runtimeAnimatorController = OverrideShieldRed;
                break;
            case RGBColor.GREEN:
                animatorShield.runtimeAnimatorController = OverrideShieldGreen;
                break;
            case RGBColor.BLUE:
                animatorShield.runtimeAnimatorController = OverrideShieldBlue;
                break;
            case RGBColor.NONE:
                break;
            default:
                break;
        }

        animator = GetComponent<Animator>();
        animator.SetBool("hasShield", true);
        animator.SetBool("isDead", isDead); // false po defaultu

        SpriteRenderer[] allSr = GetComponentsInChildren<SpriteRenderer>();
        childSr = allSr[1];
        childSr.material = defaultMaterial;


        Move();
    }

    protected override void Update()
    {
        base.Update();
        Move();
    }

    protected override void Move () {
        if (!isDead) {
            rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);

        }
        else {
            rb.velocity = Vector2.zero;
        }
    }

    public override void TakeDamage (RGBDamage damage) {
        if (hpStackList.Count == 0) {
            return;
        }

        sr.material = flashMaterial;
        childSr.material = flashMaterial;
        Tweener.Invoke(0.1f, () => sr.material = defaultMaterial);
        Tweener.Invoke(0.1f, () => childSr.material = defaultMaterial);


        HitStatus hitStatus;
        if (hpStackList[0].TakeDamage(damage, out hitStatus)) {
            //hpStackList.RemoveAt(0);
           /* if (hpStackList.Count == 0) {
                isDead = true;
                animator.SetBool("IsDead", true);
                GetComponent<BoxCollider2D>().enabled = false;
                Tweener.Invoke(.1f, () => sr.color = Color.gray);
                Move();
                Die();
            }*/
        }
        else if (hitStatus == HitStatus.BELOW_THRESHOLD) {
            animator.SetTrigger("deflect");
            animatorShield.SetTrigger("deflect");
            AudioManager.GetInstance().PlaySound(shieldDeflectEffect, true);
        }


    }

    public void Stop () {
        rb.velocity = Vector2.zero;
    }

    protected override void Die () {
        AudioManager.GetInstance().PlaySoundPitched(deathEffects[Random.Range(0, deathEffects.Count)], vocalPitch, true);

        base.Die();
    }

    public override void Flash(float duration)
    {
        sr.material = flashMaterial;
        childSr.material = flashMaterial;
        Tweener.Invoke(duration, () => sr.material = defaultMaterial);
        Tweener.Invoke(duration, () => childSr.material = defaultMaterial);
    }

    public override void SetAnimatorSpeed(float speed)
    {
        base.SetAnimatorSpeed(speed);
        animatorShield.speed = speed;
    }

    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }
}
