using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spirit_Controller : MonoBehaviour
{
    [Header("Components")]
    private PlayerControls controls;
    private Camera cam;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minHeight = 20f;
    [SerializeField] private float maxHeight = 50f;

    [Header("Movement Settings")]
    [SerializeField] public float camSpeed = 15f;
    [SerializeField] public float camRotationSpeed = 60f;
    [SerializeField] private float currentRotation = 0f;
    [SerializeField] private Vector2 currentInput = Vector2.zero;

    [Header("Interactable Objects")]
    [SerializeField] private GameObject selectedCharacter;
    [SerializeField] private GameObject targetInteractable;




    private void Awake()
    {
        cam = Camera.main;
        controls = new PlayerControls();
        controls.Enable();

        controls.Spirit.Move.performed += ctx => currentInput = ctx.ReadValue<Vector2>();
        controls.Spirit.Move.canceled += ctx => currentInput = Vector2.zero;
        controls.Spirit.Select.performed += ctx => Select();
        controls.Spirit.Interact.performed += ctx => Interact();
        controls.Spirit.Rotate.performed += ctx => currentRotation = ctx.ReadValue<float>();
        controls.Spirit.Rotate.canceled += ctx => currentRotation = 0;
        controls.Spirit.Zoom.performed += ctx => CamHeight(ctx.ReadValue<float>());

    }

    private void OnEnable()
    {
        controls.Spirit.Enable();
    }

    private void OnDisable()
    {
        controls.Spirit.Disable();
    }

    private void Update()
    {
        MoveSpirit();
        CamRotate();
    }

    private void CamHeight(float delta)
    {
        Vector3 camPosition = cam.transform.position;
        camPosition.y -= delta * zoomSpeed * Time.deltaTime;
        camPosition.y = Mathf.Clamp(camPosition.y, minHeight, maxHeight);
        cam.transform.position = camPosition;
    }

    private void CamRotate()
    {
        Vector3 camRotation = cam.transform.eulerAngles;
        camRotation.y += currentRotation * camRotationSpeed * Time.deltaTime;
        cam.transform.eulerAngles = camRotation;
    }


    private void MoveSpirit()
    {
        Vector3 movementDirection = new Vector3(currentInput.x, 0, currentInput.y);
        Vector3 localMovementDirection = cam.transform.TransformDirection(movementDirection);
        localMovementDirection.y = 0;
        Vector3 newPosition = cam.transform.position + localMovementDirection * camSpeed * Time.deltaTime;
        cam.transform.position = newPosition;
    }

    private void Select()
    { 
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject != null)
            {
                selectedCharacter = hit.collider.gameObject;
                Debug.Log("Selected");
            }
            else
            {
                 Debug.Log("Nothing Selected");
            }
        }
    }

    private void Interact()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != null)
                {
                    targetInteractable = hit.collider.gameObject;
                    Debug.Log(targetInteractable + " Selected");
                    if (selectedCharacter != null)
                    {
                        if (selectedCharacter.GetComponent<Combatant_Ai>() != null)
                        {
                            selectedCharacter.GetComponent<Combatant_Ai>().InteractWith(targetInteractable, hit.point);
                        }
                        else if (selectedCharacter.GetComponent<Worker_Ai>() != null)
                        {
                            selectedCharacter.GetComponent<Worker_Ai>().InteractWith(targetInteractable, hit.point);
                        }
                }
                }
                else
                {
                    Debug.Log("Nothing to interact with!");
                }
            }
    }
}
