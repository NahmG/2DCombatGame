using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public EnemyConfig config;
    public Logic logic;
    public EndGameUI EndGameUI;
    [SerializeField] float currentHealth;
    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] PauseMenu Menu;

    public event Action OnDeath;

    private void Start()
    {
        EndGameUI = GameObject.FindGameObjectWithTag("EndGame").GetComponent<EndGameUI>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
        currentHealth = config.maxHealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        Menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<PauseMenu>();
    }

    public void Hurt(int damage)
    {
        if(IsDead) return;

        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, config.maxHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0) 
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        animator.SetBool("IsDead", true);
        if (gameObject.name.Contains("Wizard"))
        {
            logic.AddScore(150);
        }
        if (gameObject.name.Contains("Bandit"))
        {
            logic.AddScore(100);
        }
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
        OnDeath();
        
    }

    public bool IsDead {  
        get { return animator.GetBool("IsDead"); } 
    }


}
