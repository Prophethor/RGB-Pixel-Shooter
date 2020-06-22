using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPopScript : MonoBehaviour
{
    public Vector3 anchorPopsition;

    public Vector3 spawnPosition;

    private RGBColor color;

    private HitColor colorStatus;

    public AnimatorOverrideController red;
    public AnimatorOverrideController green;
    public AnimatorOverrideController blue;

    public Animator anim;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    public void SpawnProjectile(Vector3 location, RGBColor color, HitColor colorStatus, bool nullhit)
    {
        this.gameObject.transform.position = location;
       

        //switch (color)
        //{
        //    case RGBColor.RED:
        //        anim.runtimeAnimatorController = red;
        //        break;
        //    case RGBColor.GREEN:
        //        anim.runtimeAnimatorController = green;
        //        break;
        //    case RGBColor.BLUE:
        //        anim.runtimeAnimatorController = blue;
        //        break;
        //    case RGBColor.NONE:
        //        break;
        //    default:
        //        break;
        //}

        TriggerAnimator(colorStatus, nullhit);
    }

    public void TriggerAnimator(HitColor status, bool nullhit)
    {
        if (nullhit)
        {
            anim.SetTrigger("null");
        }
        else
        {
            switch (status)
            {
                case HitColor.WRONG:
                    anim.SetTrigger("neutral");
                    break;
                case HitColor.CORRECT:
                    anim.SetTrigger("crit");
                    break;
                case HitColor.NEUTRAL:
                    anim.SetTrigger("neutral");
                    break;
                default:
                    break;
            }

        }

    }

    public void ResetPosition() // ovo se poziva iz preko notifier eventa
    {
        this.transform.position = anchorPopsition;
    }
}
