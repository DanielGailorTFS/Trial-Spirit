using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Base_StateController : MonoBehaviour
{
    private Base_AI controller;
    private Base_AnimationController ac;
    [SerializeField] private Transform attackOrigin;
    public float speed;
    public float rotationSpeed;

    private bool isProcessing;

    public State CurrentState { get { return currentState; } }
    public enum State
    {
        Movement,
        Attack,
        Dash,
        Dead,
        Ability1,
        Ability2,
        Summon,
        Dig,
        React
    }
    private State currentState;



    private void Start()
    {
        controller = gameObject.GetComponent<Base_AI>();
        ac = gameObject.GetComponent<Base_AnimationController>();
        SetState(State.Movement);
        attackOrigin = gameObject.transform;
    }

    private void Update()
    {
        HandleState(currentState);
        attackOrigin = gameObject.transform;
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
            case State.Dash:
                HandleDash();
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
            case State.Dig:
                HandleDig();
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

    private void HandleDash()
    {
        ac.AnimDash();
    }

    private void HandleDead()
    {
        ac.AnimDead();
    }

    private void HandleReact()
    {
        ac.AnimReact();
    }

    private void HandleSummon()
    {
        ac.AnimSummon();
    }

    private void HandleAttack()
    {
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

    private void HandleDig()
    {
        ac.AnimDig();
    }

    public void ProcessingEnd()
    {
        isProcessing = false;
        ac.ResetAllTriggers();
    }

    public void ProcessingStart()
    {
        isProcessing = true;
    }

}
