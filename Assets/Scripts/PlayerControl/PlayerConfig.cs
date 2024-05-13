using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="PlayerConfig", menuName ="GameConfiguration/PlayerConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    public float maxSpeed;
    public float accel;
    public bool facingRight;

    [Header("Attack")]
    public float attackRange;
    public float attackRate;
    public bool isAttacking;
    public int attackDamage;

    [Header("Dash")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    public bool isDashing;

    [Header("Stat")]
    public float maxHealth;
    public float maxStamina;
    public float chargeRate;
    public float chargeCooldown;
    public float attackCost;
    public float dashCost;  
}
