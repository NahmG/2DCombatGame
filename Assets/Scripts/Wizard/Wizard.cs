using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public DetectionZone attackZone;
    public DetectionZone dangerZone;
    public EnemyConfig config;
    public PlayerStat playerStat;
    public Transform playerTrans;
    public float horizontalMove;
    public bool _isFacingLeft;
    
    [SerializeField] Animator animator;
    [SerializeField] bool _hasTarget = false;
    [SerializeField] bool _danger = false;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Vector2 direction;

    private void Start()
    {
        _isFacingLeft = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {

        Danger = dangerZone.detectedCollider.Count > 0;
        //HasTarget = false;
        if (_danger) { HasTarget = false; }
        else { HasTarget = attackZone.detectedCollider.Count > 0; }

        if (playerStat.IsDead) return;

        if (CanMove)
        {
            TrackPlayer();
            if (horizontalMove > 0 && _isFacingLeft || horizontalMove < 0 && !_isFacingLeft)
            {
                _isFacingLeft = !_isFacingLeft;
                transform.Rotate(new Vector3(0, 180, 0));
            }   
        }
        else { horizontalMove = 0; }

        if (horizontalMove != 0)
        {
            animator.SetBool("Run", true);
        }
        else { animator.SetBool("Run", false); }

    }

    private void FixedUpdate()
    {
        if (CanMove) {
            rb.velocity = new Vector2(config.wizSpeed * horizontalMove, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.05f), rb.velocity.y);
        }
    }

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            AttackAction(value);
        }
    }

    public bool Danger
    {
        get { return _danger; }
        private set
        {
            _danger = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool("canMove"); }
    }

    void AttackAction(bool value)
    {
        animator.SetBool("HasTarget", value);
    }

    void TrackPlayer()
    {
   
        float translation = playerTrans.position.x - transform.position.x;
        direction = new Vector2(translation, 0).normalized;

        if (_danger) 
        { 
            horizontalMove = -direction.x;
        }
        else { horizontalMove = direction.x; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            dangerZone.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            dangerZone.gameObject.SetActive(true);
        }
    }

}
