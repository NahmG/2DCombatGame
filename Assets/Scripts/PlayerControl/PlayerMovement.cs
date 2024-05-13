using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class PlayerMovement: MonoBehaviour
{
    public Rigidbody2D playerBody {  get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Animator animator;
    public PlayerConfig config;

    [SerializeField] private float horizontalMove;
    [SerializeField] private float speed = 0f;
    [SerializeField] GameObject staminaBar;
    public float currentStamina;
    public Coroutine regen;
    StaminaBar _staminaBar;
    bool canDash;

    void Start()
    {
        currentStamina = config.maxStamina;
        config.facingRight = true;
        canDash = true;
        animator = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _staminaBar = staminaBar.GetComponent<StaminaBar>();    
    }

    void Update()
    {
        if (CanMove)
        {
            if (config.isDashing)
            {
                return;
            }

            if (horizontalMove < 0 && config.facingRight || horizontalMove > 0 && !config.facingRight)
            {
                config.facingRight = !config.facingRight;
                transform.Rotate(new Vector3(0, 180, 0));

            }

            if (horizontalMove != 0)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }

            speed = config.maxSpeed;
        }
        else
        {
            horizontalMove = 0;
        }
        
    }

    private void FixedUpdate()
    {
        if (config.isDashing)
        {
            return;
        }
        playerBody.velocity = new Vector2(horizontalMove * speed, playerBody.velocity.y);
    }

    public bool CanMove
    {
        get { return animator.GetBool("canMove"); }
    }

    public bool IsDead
    {
        get { return animator.GetBool("IsDead"); }
    }

    public bool CanCharge { 
        get { return animator.GetBool("canCharge"); } 
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(IsDead) return;  
        horizontalMove = context.ReadValue<Vector2>().normalized.x;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && canDash && CanMove && !IsDead && currentStamina > config.dashCost)
        {
            currentStamina -= config.dashCost;
            if (currentStamina < 0) { currentStamina = 0; }
            _staminaBar.UpdateStaminaBar(currentStamina);

            if (regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(ChargeStamina());

            animator.SetTrigger("Roll");
            StartCoroutine(DashAction());
        }
    }

    private IEnumerator DashAction()
    {
        canDash = false;
        config.isDashing = true;
        if (config.facingRight)
        {
            playerBody.velocity = new Vector2(config.dashSpeed, playerBody.velocity.y);
        }
        else
        {
            playerBody.velocity = new Vector2(-config.dashSpeed, playerBody.velocity.y);
        }

        yield return new WaitForSeconds(config.dashDuration);

        config.isDashing = false;

        yield return new WaitForSeconds(config.dashCooldown);
        canDash = true;  
    }

    public IEnumerator ChargeStamina()
    {
        yield return new WaitForSeconds(config.chargeCooldown);

        while(currentStamina < config.maxStamina)
        {
            currentStamina += config.chargeRate /10f;
            if (currentStamina > config.maxStamina) { currentStamina = config.maxStamina; }
            _staminaBar.UpdateStaminaBar(currentStamina);
            yield return new WaitForSeconds(0.01f); 
        }

        regen = null;

    }

}
