using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;

public class BloodSplatterTestScript : MonoBehaviour
{
    private Vector3 anchorPosition;
    private Vector3 destinationPosition;

    public Animator animator;
    private RGBColor color;

    public Material spriteMaterial;

  


    public void Initialize( RGBColor enemyColor, HitStatus data, Collision2D col, float emmisionValue)
    {
        spriteMaterial = GetComponent<SpriteRenderer>().material;
        switch (enemyColor)
        {
            case RGBColor.RED:
                spriteMaterial.SetColor("Color_A71320E8", Color.red*emmisionValue);
                break;
            case RGBColor.GREEN:
                spriteMaterial.SetColor("Color_A71320E8", Color.green* emmisionValue);
                break;
            case RGBColor.BLUE:
                spriteMaterial.SetColor("Color_A71320E8", Color.blue* emmisionValue);
                break;
            case RGBColor.NONE:
                spriteMaterial.SetColor("Color_A71320E8", Color.yellow * emmisionValue);
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
