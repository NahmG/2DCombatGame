using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyConfig", menuName = "GameConfiguration/EnemyConfig", order = 1)]
public class EnemyConfig : ScriptableObject
{
    [Header("Movement")]
    public float maxSpeed;
    public float wizSpeed;

    [Header("Attack")]
    public int attackDamage;

    [Header("Projectile")]
    public int projectileDamge;
    public float projectileSpeed;

    [Header("Stat")]
    public float maxHealth;
}
