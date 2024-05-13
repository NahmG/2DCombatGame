using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerAction : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public PlayerControl _inputActions;
    public Animator animator;
    public PlayerConfig config;
    public PlayerMovement movement;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    [SerializeField] GameObject staminaBar;
    StaminaBar _staminaBar;

    Vector2 direction;
    bool canAttack;

    private void Start()
    {
        canAttack = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        _staminaBar = staminaBar.GetComponent<StaminaBar>();
    }

    public bool IsDead
    {
        get { return animator.GetBool("IsDead"); }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && canAttack && !IsDead && movement.currentStamina>config.attackCost)
        {
            if (config.isDashing)
            {
                return;
            }

            movement.currentStamina -= config.attackCost;
            if (movement.currentStamina < 0) { movement.currentStamina = 0; }
            _staminaBar.UpdateStaminaBar(movement.currentStamina);
            if (movement.regen != null)
            {
                movement.StopCoroutine(movement.regen);
            }
            movement.regen = movement.StartCoroutine(movement.ChargeStamina());

            StartCoroutine(AttackAction());
        }
    }


    private IEnumerator AttackAction()
    {
        canAttack = false;

        if (config.facingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        rb.velocity = new Vector2(direction.x * 1.2f, rb.velocity.y);

        //attack animation
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1f / config.attackRate);
        canAttack = true;
    }

}
