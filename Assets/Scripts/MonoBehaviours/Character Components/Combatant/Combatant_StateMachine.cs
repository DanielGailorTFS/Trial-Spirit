using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant_StateMachine : MonoBehaviour
{
    private Base_AnimationController ac;
    public float speed;
    public float rotationSpeed;
    public float incomingDamage;
    private bool isProcessing;

    public State CurrentState { get { return currentState; } }
    public enum State
    {
        Movement,
        Attack,
        Dead,
        Ability1,
        Ability2,
        Summon,
        React
    }
    private State currentState;



    private void Start()
    {
        ac = gameObject.GetComponent<Base_AnimationController>();
        SetState(State.Movement);
    }

    private void Update()
    {
        HandleState(currentState);
        ac.isProcessing = isProcessing;
    }

    public void SetState(State newState)
    {
        currentState = newState;
        HandleState(currentState);
    }

    private void HandleState(State state)
    {
        switch (state)
        {
            case State.Movement:
                HandleMovement();
                break;
            case State.Attack:
                HandleAttack();
                break;
            case State.Dead:
                HandleDead();
                break;
            case State.Ability1:
                HandleAbility1();
                break;
            case State.Ability2:
                HandleAbility2();
                break;
            case State.Summon:
                HandleSummon();
                break;
            case State.React:
                HandleReact();
                break;
            default:
                break;
        }
    }

    private void HandleMovement()
    {
        ac.AnimMovementInput(speed, rotationSpeed);
    }

    private void HandleDead()
    {
        ac.AnimDead();
    }

    private void HandleReact()
    {
        isProcessing = true;
        if (incomingDamage <= 15)
        {
            ac.reactionLevel = 2;
        }
        else
        {
            ac.reactionLevel = 1;
        }
        ac.AnimReact();
    }

    private void HandleSummon()
    {
        ac.AnimSummon();
    }

    private void HandleAttack()
    {
        isProcessing = true;
        ac.AnimAttack();
    }

    private void HandleAbility1()
    {
        ac.AnimAbility1();
    }

    private void HandleAbility2()
    {
        ac.AnimAbility2();
    }

    public void ProcessingEnd()
    {
        Debug.Log("Combatant Processing End");
        isProcessing = false;
        ac.ResetAllTriggers();
        SetState(State.Movement);
        incomingDamage = 0;
    }

    public void ProcessingStart()
    {

        isProcessing = true;
    }

}
