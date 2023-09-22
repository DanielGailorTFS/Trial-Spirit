using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Interactable : MonoBehaviour
{
    public Transform interactionLocation;                                  // The location that the AI will move to when interacting with this interactable
    public GameObject interactableObject { get; private set; }             // The GameObject that this interactable is attached to
    public GameObject characterInteracting { get; private set; }           // The character that is currently interacting with this interactable
    public string characterType { get; private set; }                      // The type of character that is currently interacting with this interactable
    [SerializeField] public InteractableType interactableType;                              // The type of interactable this is
    public bool allowInteraction    { get; private set; }                  // Whether this interactable can be interacted with or not by the character interacting with it
    
    public enum InteractableType
    {
        Block,
        Building,
        Construction,
        Item,
        Ally,
        Enemy,
    }

    private void Awake()
    {
        interactableObject = gameObject;                                   // Set the interactableObject to the GameObject that this script is attached to
    }

    public void SetCharacterInteracting(GameObject character)
    {
        characterInteracting = character;
    }

    public void HandleInteraction(string Type)
    { 
        characterType = Type;
        switch (interactableType)
        {
            case InteractableType.Block:
                if (characterType == "Worker")
                {
                    allowInteraction = true;
                }
                else
                {
                    allowInteraction = false;
                }
                break;
            case InteractableType.Building:
                allowInteraction = true;
                break;
            case InteractableType.Construction:
                if (characterType == "Worker")
                {
                    allowInteraction = true;
                }
                else
                {
                    allowInteraction = false;
                }
                break;
            case InteractableType.Item:
                if (characterType == "Worker")
                {
                    allowInteraction = true;
                }
                else
                {
                    allowInteraction = false;
                }
                break;
            case InteractableType.Ally:
                allowInteraction = true;
                break;
            case InteractableType.Enemy:
                if (characterType == "Combat")
                {
                    allowInteraction = true;
                }
                else
                {
                    allowInteraction = false;
                }
                break;
            default:
                break;
        }
    }
}
