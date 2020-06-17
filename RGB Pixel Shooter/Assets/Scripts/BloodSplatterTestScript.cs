using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterTestScript : MonoBehaviour
{
    public Animator animator;
    private RGBColor color;

    public AnimatorOverrideController animRed;
    public AnimatorOverrideController animBlue;
    public AnimatorOverrideController animGreen;


    public void Initialize( RGBColor enemyColor, HitStatus data)
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
        
    }

    private void Awake()
    {
        animator = this.GetComponent<Animator>();

       
    }

    public void PlayAnim()
    {

    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
}
