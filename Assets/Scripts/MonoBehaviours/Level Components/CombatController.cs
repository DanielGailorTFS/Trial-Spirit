using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    enum TurnState { CombatantTurn, TrialistTurn}
    [SerializeField] private TurnState currentTurn;

    public bool combatActive = false;
    [SerializeField] private GameObject Combatant;
    [SerializeField] private GameObject Trialist;
    Combatant_Ai combatant_Ai;
    Trialist_Ai trialist_Ai;
    [SerializeField] private Combatant_StateMachine combatant_StateMachine;
    [SerializeField] private Trialist_StateMachine trialist_StateMachine;
    [SerializeField] private Base_Stats combatant_Stats;
    [SerializeField] private Base_Stats trialist_Stats;
    private bool combatProcessing = false;
    private void Start()
    {
        currentTurn = TurnState.CombatantTurn;
    }

    private void Update()
    {
        if (combatActive && !combatProcessing)
        {
            StartCoroutine(CombatSteps());
        }
    }

    public void DamageTrialist()
    {
        trialist_Stats.TakeDamage(combatant_Stats.AttackPower);
        trialist_StateMachine.incomingDamage = combatant_Stats.AttackPower;
        trialist_StateMachine.SetState(Trialist_StateMachine.State.React);

    }

    public void DamageCombatant()
    {
        combatant_Stats.TakeDamage(trialist_Stats.AttackPower);
        combatant_StateMachine.incomingDamage = trialist_Stats.AttackPower;
        combatant_StateMachine.SetState(Combatant_StateMachine.State.React);
    }

    public void StartCombat(GameObject combatant, GameObject trialist)
    {  
        Debug.Log("Combat Started");
        Combatant = combatant;
        combatant_Ai = combatant.GetComponent<Combatant_Ai>();
        combatant_StateMachine = combatant.GetComponent<Combatant_StateMachine>();
        combatant_Stats = combatant.GetComponent<Base_Stats>();

        Trialist = trialist;
        trialist_Ai = trialist.GetComponent<Trialist_Ai>();
        trialist_StateMachine = trialist.GetComponent<Trialist_StateMachine>();
        trialist_Stats = trialist.GetComponent<Base_Stats>();
        combatActive = true;

        FaceOpponent();

        combatant_StateMachine.SetState(Combatant_StateMachine.State.Attack);
    }

    private void FaceOpponent()
    {
        Vector3 directionToTrialist = Trialist.transform.position - Combatant.transform.position;
        Vector3 directionToCombatant = Combatant.transform.position - Trialist.transform.position;

        Combatant.transform.rotation = Quaternion.LookRotation(directionToTrialist);
        Trialist.transform.rotation = Quaternion.LookRotation(directionToCombatant);
    }

    public void EndCombat()
    {
        combatActive = false;
        combatant_Ai.targetEnemy = null;
        trialist_Ai.targetEnemy = null;
    }

    public void ResetStates()
    {
        Combatant = null;
        combatant_Ai = null;
        combatant_StateMachine = null;
        combatant_Stats = null;

        Trialist = null;
        trialist_Ai = null;
        trialist_StateMachine = null;
        trialist_Stats = null;
    }

    private IEnumerator CombatSteps()
    {
        combatProcessing = true;

        currentTurn = TurnState.CombatantTurn;
        yield return new WaitForSeconds(0.5f);
        FaceOpponent();
        combatant_StateMachine.SetState(Combatant_StateMachine.State.Attack);
        yield return new WaitForSeconds(3f);
        currentTurn = TurnState.TrialistTurn;
        yield return new WaitForSeconds(0.5f);
        FaceOpponent();
        trialist_StateMachine.SetState(Trialist_StateMachine.State.Attack);
        yield return new WaitForSeconds(3f);

        combatProcessing = false;
    }
}
