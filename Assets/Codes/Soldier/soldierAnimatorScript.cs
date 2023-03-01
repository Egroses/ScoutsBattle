using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldierAnimatorScript : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FireTrue()
    {
        animator.SetBool("fire", true);
    }
    public void FireFalse()
    {
        animator.SetBool("fire", false);
    }
    public void runTrue()
    {
        animator.SetBool("run", true);
    }
    public void runFalse()
    {
        animator.SetBool("run", false);
    }

    public void deadTrigger()
    {
        animator.SetTrigger("soldierDead");
    }
}
