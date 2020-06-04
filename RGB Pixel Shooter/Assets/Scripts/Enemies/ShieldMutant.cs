using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMutant : GenericEnemy {
    protected override void Start () {
        hpStackList.Add(new HPStack(baseColor, 2, 0, 0, 0, 1));
        hpStackList.Add(new HPStack(baseColor, 1));

        float yPos = PlayField.GetLanePosition(lane) - Random.Range(-0.33f, 1f) * PlayField.GetLaneHeight() / 3f;
        transform.position = new Vector3(9, yPos, yPos);

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("hasShield", true);

        animator.SetTrigger("is" + baseColor.GetString());
        animator.SetTrigger("shield" + baseColor.GetString());

        animator.SetBool("isDead", isDead);// false po defaultu

        Move();
    }

    protected override void Move () {
        rb.velocity = new Vector2(-speed * statMultipliers.GetStat(StatEnum.SPEED), 0);

        if (isDead) {
            rb.velocity = Vector2.zero;
        }
    }

    public override void TakeDamage (RGBDamage damage) {
        if (hpStackList.Count == 0) {
            return;
        }

        HitStatus hitStatus;

        if (hpStackList[0].TakeDamage(damage, out hitStatus)) {
            if (damage.color == baseColor) {
                hpStackList[0].TakeDamage(damage, out hitStatus);
                sr.material = flashMaterial;
                Invoke("ResetMaterial", .1f);
            }

            hpStackList.RemoveAt(0);
            animator.SetBool("hasShield", false);
            animator.SetTrigger("break");

            // ovde puca prvi stack, ako je prosao treshold i vratio true
            animator.SetTrigger("is" + baseColor.GetString());
            animator.SetTrigger("shield" + baseColor.GetString());

            if (hpStackList.Count == 0) {
                // ovde umire 
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false;
                animator.SetBool("isDead", isDead);

                // Temporary; TODO: change sprites to reflect behavior below
                animator.SetTrigger("is" + baseColor.GetString());
                sr.color = baseColor.GetColor();

                if (baseColor == RGBColor.BLUE) {
                    sr.color = Color.gray;
                }

                Move();
                Die();
            }

        }
        else if (!hpStackList[0].TakeDamage(damage, out hitStatus)) {
            // u slucaju da nije pukao stit, niti je umro. proveri dal je hit starus treshold, ako jeste, defelctuj
            if (hitStatus == HitStatus.BELOW_THRESHOLD) {
                animator.SetTrigger("deflect");
            }
        }
    }

    public void Stop () {
        rb.velocity = Vector2.zero;
    }

    protected override void InitiateShanking () {
        rb.velocity = Vector2.zero;
    }
}
