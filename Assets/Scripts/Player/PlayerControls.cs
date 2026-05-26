using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Combat")]
    public float meleeCooldown = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded;
    private float meleeCooldownTimer;

    private static readonly int SpeedHash  = Animator.StringToHash("Speed");
    private static readonly int MeleeHash  = Animator.StringToHash("Melee");
    private static readonly int ShootHash  = Animator.StringToHash("isShooting");
    private static readonly int GroundHash = Animator.StringToHash("isGrounded");

    void Awake()
    {
        rb   = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr   = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCombat();
        UpdateAnimator();
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h * moveSpeed, rb.linearVelocity.y);
        if (h != 0) sr.flipX = h < 0;
    }

    void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump"))
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void HandleCombat()
    {
        meleeCooldownTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && meleeCooldownTimer <= 0f)
        {
            anim.SetTrigger(MeleeHash);
            meleeCooldownTimer = meleeCooldown;
        }
    }

    void UpdateAnimator()
    {
        anim.SetFloat(SpeedHash,  Mathf.Abs(rb.linearVelocity.x));
        anim.SetBool(ShootHash,   Input.GetButton("Fire1"));
        anim.SetBool(GroundHash,  isGrounded);
    }
}