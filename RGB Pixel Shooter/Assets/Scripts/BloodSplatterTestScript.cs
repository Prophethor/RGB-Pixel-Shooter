using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterTestScript : MonoBehaviour
{
    private Vector3 anchorPosition;
    private Vector3 destinationPosition;

    public Animator animator;
    private RGBColor color;

    public AnimatorOverrideController animRed;
    public AnimatorOverrideController animBlue;
    public AnimatorOverrideController animGreen;


    public void Initialize( RGBColor enemyColor, HitStatus data, Collision2D col)
    {

        switch (enemyColor)
        {
            case RGBColor.RED:
                animator.runtimeAnimatorController = animRed;
                break;
            case RGBColor.GREEN:
                animator.runtimeAnimatorController = animGreen;
                break;
            case RGBColor.BLUE:
                animator.runtimeAnimatorController = animBlue;
                break;
            case RGBColor.NONE:
                break;
            default:
                break;
        }

        this.gameObject.transform.position = col.transform.position;

        if (data.hitColor == HitColor.CORRECT)
        {
            animator.SetTrigger("crit");


        }
        if ((data.hitColor == HitColor.NEUTRAL) || (data.hitColor == HitColor.WRONG))
        {
            animator.SetTrigger("neutral");

        }

        if (data.damageAmount <= 0 && data.belowThreshold)
        {
            animator.SetTrigger("null");
        }

    }

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        anchorPosition = this.transform.position;
       
    }


    private void ResetPosition()
    {
        this.transform.position = anchorPosition;
        animator.SetTrigger("reset");
        FindObjectOfType<ExplosionManager>().ReturnToQueue(this);
    }
}
