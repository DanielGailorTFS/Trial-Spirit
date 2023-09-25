using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Base_Stats : MonoBehaviour
{
    public Scriptable_Stats Stats;
    private Combatant_Ai combatant_Ai;
    private Trialist_Ai trialist_Ai;

    [Header("Core Attributes")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _maxMana;
    [SerializeField] private float _maxStamina;

    [Header("Dynamic Attributes")]
    [SerializeField] private float _health;
    [SerializeField] private float _mana;
    [SerializeField] private float _stamina;

    [Header("Physical Attributes")]
    [SerializeField] private float _speed;
    [SerializeField] private float _attackPower;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _defense;

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float Health { get => _health; set => _health = value; }
    public float Mana { get => _mana; set => _mana = value; }
    public float Stamina { get => _stamina; set => _stamina = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float AttackDelay { get => _attackDelay; set => _attackDelay = value; }
    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    public float Defense { get => _defense; set => _defense = value; }

    private void Start()
    {
        RetrieveStats();
    }

    private void Update()
    {
        ClampStats();
    }

    private void RetrieveStats()
    {
        _maxHealth = Stats.maxHealth;
        _maxMana = Stats.maxMana;
        _maxStamina = Stats.maxStamina;
        _speed = Stats.speed;
        _attackPower = Stats.attackPower;
        _attackDelay = Stats.attackDelay;
        _defense = Stats.defense;
        _health = _maxHealth;
        _mana = _maxMana;
        _stamina = _maxStamina;
    }

    private void ClampStats()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _mana = Mathf.Clamp(_mana, 0, _maxMana);
        _stamina = Mathf.Clamp(_stamina, 0, _maxStamina);
    }


    public void TakeDamage(float damage)
    {
        float effectiveDamage = damage - Defense;
        if (effectiveDamage < 0)
            effectiveDamage = 0;
        Health -= effectiveDamage;

        if (Health <= 0)
        {
            if (gameObject.tag == "Combatant")
            {
                combatant_Ai = gameObject.GetComponentInParent<Combatant_Ai>();
                combatant_Ai.Die();

            }
            else if (gameObject.tag == "Trialist")
            {
                trialist_Ai = gameObject.GetComponentInParent<Trialist_Ai>();
                trialist_Ai.Die();
            }
        }
    }





}
