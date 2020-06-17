using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMutant : GenericEnemy {

    private SpriteRenderer childSr;

    protected override void Start () {
        RGBColor randColor = (RGBColor)Random.Range(0, 2);
        hpStackList.Add(new HPStack(randColor, 2, 0, 0, 0, 2));
        hpStackList.Add(new HPStack(baseColor, 5));



        hpStackList[0].SetOnDestroy(() => {
            hpStackList.RemoveAt(0);
            sr.material = flashMaterial;
            childSr.material = flashMaterial;
            Invoke("ResetMaterial", .1f);

            // ovde puca prvi stack, ako je prosao treshold i vratio true
            animator.SetTrigger("break");
            animator.SetBool("hasShield", false);
            animator.SetTrigger("is" + baseColor.GetString());
            animator.SetTrigger("shield" + randColor.GetString());
        });
        hpStackList[1].SetOnDestroy(() => {
            
            hpStackList.RemoveAt(0);
        });

        float yPos = PlayField.GetLanePosition(lane) - Random.Range(-0.33f, 1f) * PlayField.GetLaneHeight() / 3f;
        transform.position = new Vector3(9, yPos, yPos);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetTrigger("is" + baseColor.GetString());
        animator.SetTrigger("shield" + randColor.GetString());
        animator.SetBool("hasShield", true);
        animator.SetBool("isDead", isDead); // false po defaultu

        SpriteRenderer[] allSr = GetComponentsInChildren<SpriteRenderer>();
        childSr = allSr[1];
        childSr.material = defaultMaterial;
        Move();
    }

    protected override void Move () {
        rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);

        if (isDead) {
            rb.velocity = Vector2.zero;
        }
    }

    public override void TakeDamage (RGBDamage damage) {
        HitStatus hitStatus;
        bool hitBool = hpStackList[0].TakeDamage(damage, out hitStatus);


<<<<<<< Updated upstream
        
        if (!hitBool) {
            // u slucaju da nije pukao stit, niti je umro. proveri dal je hit status treshold, ako jeste, defelctuj
            if (hitStatus.belowThreshold) {
                animator.SetTrigger("deflect");
=======
        if (hpStackList[0].TakeDamage(damage, out hitStatus)) // if true happenede (3 cases, neutral, wrong color, correct color)
        {
            switch (hitStatus)
            {
                case HitStatus.HIT_WRONG_COLOR:
                    Debug.Log(hitStatus);
                    sr.material = flashMaterial;
                    childSr.material = flashMaterial;
                    Invoke("ResetMaterial", .1f);
                    break;
                case HitStatus.HIT_CORRECT:
                    Debug.Log(hitStatus);
                    sr.material = flashMaterial;
                    childSr.material = flashMaterial;
                    Invoke("ResetMaterial", .1f);
                    break;
                case HitStatus.HIT_NEUTRAL:
                    Debug.Log(hitStatus);
                    sr.material = flashMaterial;
                    childSr.material = flashMaterial;
                    Invoke("ResetMaterial", .1f);
                    break;
                default:
                    break;
>>>>>>> Stashed changes
            }

<<<<<<< Updated upstream

        if (!animator.GetBool("hasShield") && hitBool) {
            sr.material = flashMaterial;
            Invoke("ResetMaterial", .1f);
        }
=======
            if (animator.GetBool("hasShield"))
            {
                hpStackList.RemoveAt(0);
                sr.material = flashMaterial;
                childSr.material = flashMaterial;
                Invoke("ResetMaterial", .1f);

                // ovde puca prvi stack, ako je prosao treshold i vratio true
                animator.SetTrigger("break");
                animator.SetBool("hasShield", false);
                animator.SetTrigger("is" + baseColor.GetString());
                animator.SetTrigger("shield" + baseColor.GetString());
            }

            if (!animator.GetBool("hasShield"))
            {
                if (hpStackList[0].GetAmount() > 0)
                {
                    sr.material = flashMaterial;
                    Invoke("ResetMaterial", .1f);
                }
                else
                {
                    hpStackList.RemoveAt(0);
                }
            }
>>>>>>> Stashed changes

            if (hpStackList.Count == 0)
            {
                // ovde umire 
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false;
                animator.SetBool("isDead", isDead);

<<<<<<< Updated upstream
            // Temporary; TODO: change sprites to reflect behavior below
            animator.SetTrigger("is" + baseColor.GetString());

=======
                // Temporary; TODO: change sprites to reflect behavior below
                animator.SetTrigger("is" + baseColor.GetString());
>>>>>>> Stashed changes

                Move();
                Die();
            }
        }
        else
        {
            switch (hitStatus)
            {
                case HitStatus.BELOW_THRESHOLD:
                    Debug.Log(hitStatus);
                    animator.SetTrigger("deflect");
                    break;
                case HitStatus.INSUFFICIENT_DAMAGE:
                    Debug.Log(hitStatus);
                    break;
                default:
                    break;
            }
        }
    }


    public void Stop () {
        rb.velocity = Vector2.zero;
    }

    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }

    public override void ResetMaterial () {
        base.ResetMaterial();
        childSr.material = defaultMaterial;
    }
}
