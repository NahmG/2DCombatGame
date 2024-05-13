using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public EnemyConfig config;
    public PlayerConfig playerConfig;
    public GameObject player;
    public bool facingLeft;
    public float horizontalMove;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    private void Start()
    {
        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, 0f).normalized;
        if(facingLeft ) { horizontalMove = -1f; }
        else { 
            horizontalMove = 1f;
            transform.localScale = new Vector3(-1,1,1);
        }

        
        rb.velocity = new Vector2(config.projectileSpeed * horizontalMove, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerConfig.isDashing)
        {
            return;
        }

        GameObject target = collision.gameObject;

        if (target != null)
        {
            target.GetComponent<PlayerStat>().Hurt(config.projectileDamge);
            Destroy(gameObject);
        }
    }
}
