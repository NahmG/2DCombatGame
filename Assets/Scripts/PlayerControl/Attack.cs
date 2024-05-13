using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public PlayerConfig playerConfig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemy = collision.gameObject;
        enemy.GetComponent<EnemyHealth>().Hurt(playerConfig.attackDamage);
    }

}
