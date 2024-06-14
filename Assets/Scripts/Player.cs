using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public float health = 50f;
    private float maxHealth;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isFacingRight = true;
    private float moveInput;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    private bool wasGrounded = true;

    [Header("Double Jump Settings")]
    public bool canDoubleJump = false;
    public int maxJumps = 2;
    private int jumpCount = 1;
    private bool isJumping;

    [Header("Key")]
    public bool hasKey = false;

    [HideInInspector] public Rigidbody2D _rigidBody;
    [HideInInspector] public Animator _animator;

    // STATIC STRINGS
    private const string IDLE_ANIM = "Idle";
    private const string RUN_ANIM = "Run";
    private const string JUMP_ANIM = "Jump";
    private const string FALL_ANIM = "Fall";
    private const string TAKE_DAMAGE_ANIM = "Take Damage";
    private const string DEATH_ANIM = "Death";

    void Start()
    {
        maxHealth = health;
        hasKey = false;


        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get horizontal movement input
        moveInput = Input.GetAxis("Horizontal");

        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 1;
        }

        // Flip player
        if (moveInput > 0) // Facing Right
        {
            isFacingRight = false;
            Flip();
        }
        else if (moveInput < 0) // Facing Left
        {
            isFacingRight = true;
            Flip();
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < maxJumps))
        {
            isJumping = true;
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        _rigidBody.velocity = new Vector2(moveInput * moveSpeed, _rigidBody.velocity.y);

        // Apply jump force
        if (isJumping)
        {
            jumpCount++;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            isJumping = false;
        }
    }

    void UpdateAnimations()
    {
        _animator.SetBool(JUMP_ANIM, false);
        _animator.SetBool(FALL_ANIM, false);
        _animator.SetBool(IDLE_ANIM, false);
        _animator.SetBool(RUN_ANIM, false);

        if (!isGrounded)
        {
            if (_rigidBody.velocity.y > 0)
            {
                _animator.SetBool(JUMP_ANIM, true);
            }
            else
            {

                _animator.SetBool(FALL_ANIM, true);
            }
        }
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            _animator.SetBool(RUN_ANIM, true);
        }
        else
        {
            _animator.SetBool(IDLE_ANIM, true);
        }

        //if (isGrounded && !wasGrounded)
        //{
        //    _animator.SetTrigger(LAND_ANIM);
        //}

        //wasGrounded = isGrounded;
    }

    void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (isFacingRight)
            rotation.y = 180f;
        else
            rotation.y = 0f;

        transform.eulerAngles = rotation;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        //healthImg.fillAmount = health / maxHealth;

        if (health <= 0)
        {
            _rigidBody.velocity = Vector2.zero;
            //_rigidBody.isKinematic = true;
            //GetComponent<CapsuleCollider2D>().enabled = false;
            _animator.SetTrigger(DEATH_ANIM);
            StartCoroutine(CallDeath(3f));
            return;
        }

        _animator.SetTrigger(TAKE_DAMAGE_ANIM);
    }

    private IEnumerator CallDeath(float delay)
    {
        this.enabled = false;
        yield return new WaitForSeconds(delay/2);
        GameManager.Instance.RestartGame();
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        //if (collision.gameObject.CompareTag("SpikeBall"))
        //{
        //    StartCoroutine(CallDeath(3f));
        //}
    }

    void OnDrawGizmos()
    {
        // Draw the ground check radius in the editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
