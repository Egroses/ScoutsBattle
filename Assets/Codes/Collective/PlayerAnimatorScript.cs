using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RunTrue()
    {
        animator.SetBool("Run",true);
    }
    public void RunFalse()
    {
        animator.SetBool("Run",false);
    }
    public void CaryTrue()
    {
        animator.SetBool("Cary",true);
    }
    public void CaryFalse()
    {
        animator.SetBool("Cary",false);
    }
    public void deadTrigger()
    {
        animator.SetTrigger("playerDead");
    }
    public void NewLevelAnimator()
    {
        animator.SetTrigger("newLevel");
    }
}
