using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterTestScript : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
}
