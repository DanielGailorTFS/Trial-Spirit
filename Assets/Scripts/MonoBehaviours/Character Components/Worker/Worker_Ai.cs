using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker_Ai : MonoBehaviour
{
    // This is the base class for all AI, it handles movement and interaction with interactables
    private Base_Stats stats;                                      // Reference to the Stats component
    private Worker_StateMachine sm;                               // Reference to the StateController for AI
    private NavMeshAgent agent;                                    // Reference to the NavMeshAgent component
    public float inputHoldDelay = 0.5f;                            // How long after reaching an interactable before input is allowed again
    public float interactionDistance = 1f;                         // The distance from the interactionLocation of an interactable that the AI will stop at

    private Base_Interactable targetInteractable;                  // Reference to the current interactable that we are headed towards
    //private bool handleInput = true;                               // Whether input is currently being handled.
    private WaitForSeconds inputHoldWait;                          // Used to store the WaitForSeconds needed for inputHoldDelay. The WaitForSeconds used to make the user wait before input is handled again.
    private Vector3 localVelocity;                                 // The velocity of the AI relative to it's own rotation

    public bool enemyDetected;                                     // Whether an enemy has been detected or not
    public GameObject targetEnemy;                                 // The enemy that is currently being targeted
    public string aiType = "Worker"; 


    private void Start()
    {
        sm = gameObject.GetComponent<Worker_StateMachine>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        inputHoldWait = new WaitForSeconds(inputHoldDelay);
    }

    private void Update()
    {
        UpdateMoveInput();
        InteractionCheck();
    }

    private void UpdateMoveInput()
    {
        localVelocity = transform.InverseTransformDirection(agent.velocity);
        float normalizedSpeed = Mathf.Clamp(localVelocity.z, -1, 1);
        float normalizedAngularSpeed = Mathf.Clamp(localVelocity.x, -1, 1);
        sm.speed = normalizedSpeed;
        sm.rotationSpeed = normalizedAngularSpeed;
    }

    public void InteractWith(GameObject targetInteractable, Vector3 clickedPosition)
    {
        Base_Interactable interactableComponent = targetInteractable.GetComponent<Base_Interactable>();
        if (interactableComponent != null)
        {
            interactableComponent.SetCharacterInteracting(gameObject);
            float distance = Vector3.Distance(transform.position, targetInteractable.transform.position);
            Debug.Log("Distance to interactable: " + distance);
            if (distance >= interactionDistance)
            {

                Debug.Log("Interacting with " + targetInteractable.name);
                ProcessInteraction(interactableComponent);
            }
            agent.SetDestination(targetInteractable.transform.position);
        }
        else
        {
            agent.SetDestination(clickedPosition);
        }
    }

    private void InteractionCheck()
    {
        if (targetInteractable != null)
        {
            float distance = Vector3.SqrMagnitude(transform.position - targetInteractable.transform.position);
            float interactionSqrDistance = interactionDistance * interactionDistance;
            if (distance <= interactionSqrDistance)
            {
                WaitForSeconds inputHoldWait = new WaitForSeconds(inputHoldDelay);
                Debug.Log("Interacting with " + targetInteractable.name);
                ProcessInteraction(targetInteractable);
            }
            else
            {
                Debug.Log("Squared Distance to interactable: " + distance);
            }
        }
    }

    private void ProcessInteraction(Base_Interactable interactable)
    {
        Debug.Log("Processing Interaction");
        targetInteractable = interactable;
        Base_Interactable.InteractableType interactableType = targetInteractable.interactableType;
        Debug.Log(targetInteractable);

        switch (interactableType)
        {
            case Base_Interactable.InteractableType.Block:
                Dig();
                break;
            case Base_Interactable.InteractableType.Building:
                UseBuilding();
                break;
            case Base_Interactable.InteractableType.Construction:
                Construct();
                break;
            case Base_Interactable.InteractableType.Item:
                Carry();
                break;
            case Base_Interactable.InteractableType.Ally:
                HelpAlly();
                break;
            case Base_Interactable.InteractableType.Enemy:
                break;
            default:
                Debug.Log("Unhandled interactable type: " + interactableType);
                break;
        }
    }

    private void IdleWander()
    {
        Debug.Log("Idling");
    }

    private void Dig()
    {
        Debug.Log("Digging");
        if (targetInteractable != null && targetInteractable.CompareTag("Base_Block"))
        {
            Base_Block block = targetInteractable.GetComponent<Base_Block>();
            if (block != null)
            {
                block.WallHit();
                sm.SetState(Worker_StateMachine.State.Dig);
            }
            else
            {
                Debug.Log("Block component is missing!");
            }
        }
        else
        {
            Debug.Log("Target interactable is null or not a block!");
        }
    }

    private void UseBuilding()
    {
        Debug.Log("Using Building");
    }

    private void Construct()
    {
        Debug.Log("Constructing");
    }

    private void Carry()
    {
        Debug.Log("Carrying");
    }

    private void HelpAlly()
    {
        Debug.Log("Helping Ally");
    }

    public void DetectEnemy(GameObject newTarget)
    {
        targetEnemy = newTarget;
        Debug.Log("Enemy detected: " + targetEnemy.name);

        if (targetEnemy != null)
        {
            
        }
    }
}
