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

    public void AnimDead() { anim.SetTrigger("Dead");  }

    public void AnimAttack() { anim.SetTrigger("Attack"); }

    public void AnimAbility1() { anim.SetTrigger("Ability1"); }

    public void AnimAbility2() { anim.SetTrigger("Ability2"); }

    public void AnimDig() { anim.SetTrigger("Dig"); }

    public void AnimReact()
    {
        if (reactionLevel == 1) { anim.SetTrigger("smallReact"); }
        else if (reactionLevel == 2) { anim.SetTrigger("largeReact"); }
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
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Dead");
        anim.ResetTrigger("smallReact");
        anim.ResetTrigger("largeReact");
        anim.ResetTrigger("Summon");
        reactionLevel = 0;
    }


}
