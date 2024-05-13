using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public PlayerConfig config;
    [SerializeField]Animator animator;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject menu;
    PauseMenu pauseMenu;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
        currentHealth = config.maxHealth;
        animator = GetComponent<Animator>();    
        pauseMenu = menu.GetComponent<PauseMenu>();
    }
    
    public bool IsDead
    {
        get { return animator.GetBool("IsDead"); }
    }

    public void Hurt(int damage)
    {
        if(IsDead) return;

        currentHealth -= damage;

        healthBar.GetComponent<HealthBar>().UpdateHealthBar(currentHealth, config.maxHealth);

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0) 
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        animator.SetBool("IsDead", true);
        Physics2D.IgnoreLayerCollision(7, 8);
        yield return new WaitForSeconds(3);
        pauseMenu.GameOver();
    }
}
