using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trialist_Ai : MonoBehaviour
{
    // This is the base class for all AI, it handles movement and interaction with interactables
    private Base_Stats stats;                                      // Reference to the Stats component
    private Trialist_StateMachine sm;                               // Reference to the StateController for AI
    private NavMeshAgent agent;                                    // Reference to the NavMeshAgent component
    public float inputHoldDelay = 0.5f;                            // How long after reaching an interactable before input is allowed again
    public float interactionDistance = 1.5f;                         // The distance from the interactionLocation of an interactable that the AI will stop at

    private Base_Interactable targetInteractable;                  // Reference to the current interactable that we are headed towards
    //private bool handleInput = true;                               // Whether input is currently being handled.
    private WaitForSeconds inputHoldWait;                          // Used to store the WaitForSeconds needed for inputHoldDelay. The WaitForSeconds used to make the user wait before input is handled again.
    private Vector3 localVelocity;                                 // The velocity of the AI relative to it's own rotation

    public bool enemyDetected;                                     // Whether an enemy has been detected or not
    public GameObject targetEnemy;                                 // The enemy that is currently being targeted
    public string aiType;
    public bool isAlive = true;
    public GameObject dropPrefab;

    [SerializeField] private CombatController combatController;


    private void Start()
    {
        sm = gameObject.GetComponent<Trialist_StateMachine>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        stats = gameObject.GetComponent<Base_Stats>();
        combatController = GameObject.Find("LevelManager").GetComponent<CombatController>();
        inputHoldWait = new WaitForSeconds(inputHoldDelay);
        aiType = gameObject.tag;
    }

    private void Update()
    {
        if (isAlive)
        {
            UpdateMoveInput();
            InteractionCheck();
            ChaseEnemy();
        }
        
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
            case Base_Interactable.InteractableType.Building:
                UseBuilding();
                break;
            case Base_Interactable.InteractableType.Enemy:
                targetEnemy = targetInteractable.gameObject;
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
    private void UseBuilding()
    {
        Debug.Log("Using Building");
    }

    private void StartCombat()
    {
        //combatController.StartCombat(targetEnemy, gameObject);
    }

    public void CombatDamage()
    {
        combatController.DamageCombatant();
    }

    public void EndAction()
    {
        sm.ProcessingEnd();
    }

    private void ChaseEnemy()
    {
        if (targetEnemy != null)
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= interactionDistance)
            {
                agent.SetDestination(transform.position);
                //StartCombat();

            }
            else
            {
                agent.SetDestination(targetEnemy.transform.position);
            }
        }
    }

    public void Die()
    {
        Debug.Log("Trialist Died");
        sm.ProcessingEnd();
        isAlive = false;
        sm.SetState(Trialist_StateMachine.State.Dead);
        combatController.EndCombat();
        DropLoot();
    }

    private void DropLoot()
    {
        GameObject droppedItem = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        StartCoroutine(MoveToCamera(droppedItem));
    }

    private void CompleteLevel()
    {
        Debug.Log("Level Complete");
        Level_Manager levelManager = GameObject.Find("LevelManager").GetComponent<Level_Manager>();
        levelManager.LevelComplete();
    }

    private IEnumerator MoveToCamera(GameObject droppedItem)
    {
        Vector3 startPosition = droppedItem.transform.position;
        Vector3 endPosition = Camera.main.transform.position;
        float travelTime = 10.0f;
        float startTime = Time.time;
        Debug.Log("Starting Move to camera");
        
        while (Vector3.Distance(droppedItem.transform.position, endPosition) > 0.1f)
        {
            float journeyFraction = (Time.time - startTime) / travelTime;
            droppedItem.transform.position = Vector3.Lerp(startPosition, endPosition, journeyFraction);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Destroying dropped item");
        Destroy(droppedItem);
        CompleteLevel();
    }

    public void DetectEnemy(GameObject newTarget)
    {
        targetEnemy = newTarget;
        Debug.Log("Enemy detected: " + targetEnemy.name);
    }
}
