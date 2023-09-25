using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Character/Stats")]
public class Scriptable_Stats : ScriptableObject
{
    public float maxHealth;
    public float maxMana;
    public float maxStamina;
    public float speed;
    public float attackPower;
    public float attackDelay;
    public float defense;
}
