using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Base_Interactable : MonoBehaviour
{
    public Transform interactionLocation;                                  // The location that the AI will move to when interacting with this interactable
    public GameObject interactableObject;             // The GameObject that this interactable is attached to
    public GameObject characterInteracting;           // The character that is currently interacting with this interactable
    [SerializeField] public InteractableType interactableType;                              // The type of interactable this is
    
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
}