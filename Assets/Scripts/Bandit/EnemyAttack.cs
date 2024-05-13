using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{ 
    public EnemyConfig enemyConfig;
    public PlayerConfig config;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (config.isDashing)
        {
            return;
        }
        target.GetComponent<PlayerStat>().Hurt(enemyConfig.attackDamage);
    }
}
