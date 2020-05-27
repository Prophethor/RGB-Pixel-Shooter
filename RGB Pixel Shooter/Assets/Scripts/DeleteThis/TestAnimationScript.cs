using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationScript : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            animator1.SetBool("Win", true);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator1.SetBool("Lose", true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator1.SetBool("Lose", false);
            animator1.SetBool("Win", false);
        }
    }
}
