using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_StateMachine : MonoBehaviour
{
    private Worker_Ai controller;
    private Base_AnimationController ac;
    [SerializeField] private Transform attackOrigin;
    public float speed;
    public float rotationSpeed;

    private bool isProcessing;

    public State CurrentState { get { return currentState; } }
    public enum State
    {
        Movement,
        Dead,
        Ability1,
        Summon,
        Dig,
        React
    }
    private State currentState;

    private void Start()
    {
        controller = gameObject.GetComponent<Worker_Ai>();
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
            case State.Dead:
                HandleDead();
                break;
            case State.Ability1:
                HandleAbility1();
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

    private void HandleAbility1()
    {
        ac.AnimAbility1();
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
