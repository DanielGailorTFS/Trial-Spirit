using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_AnimationController : MonoBehaviour
{
    private Animator anim;
    public int reactionLevel;
    public bool isProcessing;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetInteger("reactionLevel", reactionLevel);
        anim.SetBool("isProcessing", isProcessing);
    }

    public void AnimMovementInput(float speed, float rotationSpeed)
    {
        anim.SetFloat("forward", speed);
        anim.SetFloat("sideways", rotationSpeed);
    }

    public void AnimDash() { anim.SetTrigger("Dash"); }

    public void AnimDead() { anim.SetTrigger("Dead");  }

    public void AnimAttack() { anim.SetTrigger("Attack"); }

    public void AnimAbility1() { anim.SetTrigger("Ability1"); }

    public void AnimAbility2() { anim.SetTrigger("Ability2"); }

    public void AnimDig() { anim.SetTrigger("Dig"); }

    public void AnimReact()
    {
        if (reactionLevel == 1) { anim.SetTrigger("SmallReact"); }
        else if (reactionLevel == 2) { anim.SetTrigger("BigReact"); }
        else
        {
            Debug.Log("Reaction Level not set");
            return;
        }
    }

    public void AnimSummon()
    {
        anim.SetTrigger("Summon");
    }

    public void ResetAllTriggers()
    {
        anim.ResetTrigger("Dash");
        anim.ResetTrigger("Dead");
        anim.ResetTrigger("SmallReact");
        anim.ResetTrigger("BigReact");
        anim.ResetTrigger("Summon");
        reactionLevel = 0;
    }


}
