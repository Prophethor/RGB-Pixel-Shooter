using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMutant : GenericEnemy {

    public AudioClip shieldBreak;
    public AudioClip shieldDeflect;

    private SpriteRenderer childSr;

    public AnimatorOverrideController OverrideRed;
    public AnimatorOverrideController OverrideBlue;
    public AnimatorOverrideController OverrideGreen;

    public AnimatorOverrideController OverrideShieldRed;
    public AnimatorOverrideController OverrideShieldBlue;
    public AnimatorOverrideController OverrideShieldGreen;

    private Animator animatorShield;
    public GameObject HP1;
    public GameObject HP2;

    protected override void Start() {

        AudioManager.instance.PlaySoundPitched(spawnClips[Random.Range(0, spawnClips.Count)], 1f);

        RGBColor randcolor = (RGBColor)Random.Range(0, 3);
        hpStackList.Add(new HPStack(randcolor, 500, 0, 0, 0, 500));
        hpStackList[0].SetHPBar(HP1);
        hpStackList.Add(new HPStack(baseColor, 100));
        hpStackList[1].SetHPBar(HP2);

        animatorShield = GetComponentsInChildren<Animator>()[1]; // assuming that [0] is main char animator, and 1 is first child animator


        hpStackList[0].SetOnDestroy(() => {
            hpStackList.RemoveAt(0);
            Tweener.Invoke(0.1f, () => sr.material = defaultMaterial);
            Tweener.Invoke(0.1f, () => childSr.material = defaultMaterial);

            // ovde puca prvi stack, ako je prosao treshold i vratio true
            animator.SetTrigger("break");
            animatorShield.SetTrigger("break");
            animator.SetBool("hasShield", false);
            AudioManager.instance.PlaySoundPitched(shieldBreak, 1f);
        });
        hpStackList[1].SetOnDestroy(() => {

            hpStackList.RemoveAt(0);
            Tweener.Invoke(0.1f, () => sr.material = defaultMaterial);
        });

        float yPos = PlayField.GetLanePosition(lane) - Random.Range(-0.33f, 1f) * PlayField.GetLaneHeight() / 3f;
        transform.position = new Vector3(9, yPos, yPos);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();

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


    protected override void Move () {
        if (!isDead) {
            rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);

        }
        else {
            rb.velocity = Vector2.zero;
        }
    }

    public override HitStatus TakeDamage (RGBDamage damage) {
        HitStatus hitStatus;
        bool hitBool = hpStackList[0].TakeDamage(damage, out hitStatus);

        
        if (!hitBool) {
            // u slucaju da nije pukao stit, niti je umro. proveri dal je hit status treshold, ako jeste, defelctuj
            if (hitStatus.belowThreshold) {
                animator.SetTrigger("deflect");
                animatorShield.SetTrigger("deflect");
                AudioManager.instance.PlaySound(shieldDeflect, true);

                sr.material = flashMaterial;
                childSr.material = flashMaterial;
                Tweener.Invoke(0.1f, () => sr.material = defaultMaterial);
                Tweener.Invoke(0.1f, () => childSr.material = defaultMaterial);
            }
            else Debug.Log(hitStatus); // ovo ne bi trebalo da se desi u principu
        }


        if (!animator.GetBool("hasShield") && hitBool) {
            sr.material = flashMaterial;
            childSr.material = flashMaterial;
            Tweener.Invoke(0.1f, () => sr.material = defaultMaterial);
            Tweener.Invoke(0.1f, () => childSr.material = defaultMaterial);

        }

        if (hpStackList.Count == 0) {
            // ovde umire 
            isDead = true;
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("isDead", isDead);
            Move();
            Die();
   
        }
        return hitStatus;
    }


    public void Stop () {
        rb.velocity = Vector2.zero;
    }

    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }
    

    
}
