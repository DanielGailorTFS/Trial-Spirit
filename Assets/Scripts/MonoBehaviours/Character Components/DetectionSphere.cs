using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{
    public GameObject character;
    public Combatant_Ai combatantAi;
    public Worker_Ai workerAi;
    public Trialist_Ai trialistAi;
    public GameObject enemy;

    // Start is called before the first frame update
    private void Start()
    {
        character = gameObject.transform.parent.gameObject;
        if (character.GetComponent<Combatant_Ai>() != null)
        {
            combatantAi = character.GetComponent<Combatant_Ai>();
        }
        else if (character.GetComponent<Worker_Ai>() != null)
        {
            workerAi = character.GetComponent<Worker_Ai>();
        }
        else if (character.GetComponent<Trialist_Ai>() != null)
        {
            trialistAi = character.GetComponent<Trialist_Ai>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter Called. Other object's tag: " + other.gameObject.tag);
        Debug.Log("Other: " + other.gameObject.name);


        if (character.GetComponent<Combatant_Ai>() != null)
        {
            if (character.CompareTag("Combatant") && other.gameObject.CompareTag("Trialist"))
            {
                enemy = other.gameObject;
                combatantAi.enemyDetected = true;
                combatantAi.DetectEnemy(other.gameObject);
            }
        }
        else if (character.GetComponent<Worker_Ai>() != null)
        {
            if (character.CompareTag("Worker") && other.gameObject.CompareTag("Trialist"))
            {
                enemy = other.gameObject;
                workerAi.enemyDetected = true;
                workerAi.DetectEnemy(other.gameObject);
            }
        }
        else if (character.GetComponent<Trialist_Ai>() != null)
        {
            if (character.CompareTag("Trialist") && other.gameObject.CompareTag("Combatant"))
            {
                enemy = other.gameObject;
                trialistAi.enemyDetected = true;
                trialistAi.DetectEnemy(other.gameObject);
            }
        }
    }

}
