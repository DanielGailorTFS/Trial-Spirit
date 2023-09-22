using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Base_AI : MonoBehaviour
{
    // This is the base class for all AI, it handles movement and interaction with interactables
    private Base_Stats stats;                                      // Reference to the Stats component
    private Base_StateController sc;                               // Reference to the StateController for AI
    private NavMeshAgent agent;                                    // Reference to the NavMeshAgent component
    public float inputHoldDelay = 0.5f;                            // How long after reaching an interactable before input is allowed again
    public float interactionDistance = 1f;                         // The distance from the interactionLocation of an interactable that the AI will stop at

    private Base_Interactable targetInteractable;                  // Reference to the current interactable that we are headed towards
    private Vector3 destinationPosition;                           // The position that is currently being headed towards, this is the interactionLocation of the currentInteractable if it is not null.
    private bool handleInput = true;                               // Whether input is currently being handled.
    private WaitForSeconds inputHoldWait;                          // Used to store the WaitForSeconds needed for inputHoldDelay. The WaitForSeconds used to make the user wait before input is handled again.
    private Vector3 localVelocity;                                 // The velocity of the AI relative to it's own rotation

    private const float stopDistanceProportion = 0.1f;             // Proportion of the NavMeshAgent's stopping distance within which the player stops completely when reaching a waypoint or an interactable.
    private const float navMeshSampleDistance = 4f;                // The maximum distance from the nav mesh a click can be to be accepted.

    private bool isCombatEntity;                                   // Whether this AI is a combat entity or not, used to handle interactions differently based on type

    // Make AI type; combat, worker, spirit, trialist
    public enum AIType
    {
        Combat,
        Worker,
        Trialist
    }
    public AIType aiType { get; private set; }


    private void Start()
    {
        sc = gameObject.GetComponent<Base_StateController>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        inputHoldWait = new WaitForSeconds(inputHoldDelay);

        destinationPosition = transform.position;
    }

    private void Update()
    {
        UpdateMoveInput();
    }

    private void UpdateMoveInput()
    { 
        localVelocity = transform.InverseTransformDirection(agent.velocity);
        float normalizedSpeed = Mathf.Clamp(localVelocity.z, -1, 1);
        float normalizedAngularSpeed = Mathf.Clamp(localVelocity.x, -1, 1);
        sc.speed = normalizedSpeed;
        sc.rotationSpeed = normalizedAngularSpeed;
    }

    public void InteractWith(GameObject targetInteractable, Vector3 clickedPosition)
    {
        Base_Interactable interactableComponent = targetInteractable.GetComponent<Base_Interactable>();
        if (interactableComponent != null)
        {
            interactableComponent.SetCharacterInteracting(gameObject);
            float distance = Vector3.Distance(transform.position, targetInteractable.transform.position);
            if (distance <= interactionDistance)
            {
                interactableComponent.HandleInteraction(aiType.ToString());
                if (interactableComponent.allowInteraction)
                {
                    ProcessInteraction(interactableComponent);
                }
            }
            agent.SetDestination(targetInteractable.transform.position);
        }
        else
        {
            agent.SetDestination(clickedPosition);
        }
    }

    private void ProcessInteraction(Base_Interactable interactable)
    { 
        Debug.Log("Processing Interaction");
        string interactableType = interactable.interactableType.ToString();
        Debug.Log(interactableType);

        switch (interactableType)
        {
            case "Block":
                WorkerDig();
                break;
            case "Building":
                UseBuilding();
                break;
            case "Construction":
                WorkerConstruct();
                break;
            case "Item":
                WorkerCarry();
                break;
            case "Ally":
                //HelpAlly();
                break;
            case "Enemy":
                AttackEnemy();
                break;
        }
    }

    private void WorkerDig()
    {
        Debug.Log("Digging");
        if (targetInteractable.CompareTag("Block"))
        {
            Base_Block block = targetInteractable.GetComponent<Base_Block>();
            block.WallHit();
            sc.SetState(Base_StateController.State.Dig);
        }
    }

    private void UseBuilding()
    {
        Debug.Log("Using Building");
    }

    private void WorkerConstruct()
    {
        Debug.Log("Constructing");
    }

    private void WorkerCarry()
    {
        Debug.Log("Carrying");
    }

    private void HelpAlly()
    {
        Debug.Log("Helping Ally");
    }

    private void AttackEnemy()
    {
        Debug.Log("Attacking Enemy");
    }



}
