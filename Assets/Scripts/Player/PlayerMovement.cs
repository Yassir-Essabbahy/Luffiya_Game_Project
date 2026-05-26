using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float direction = 0;
    public float speed = 250;
    public bool isFacingRight = true;

    public float jumpForce = 7;
    bool isGrounded;
    int numberOfJumps = 0;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Rigidbody2D playerRB;
    public Animator animator;

    private void Awake()
    {
        if (playerRB == null) playerRB = GetComponent<Rigidbody2D>();
        if (animator == null) animator  = GetComponent<Animator>();
    }

void FixedUpdate()
{
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    animator.SetBool("isGrounded", isGrounded);

    direction = Input.GetAxisRaw("Horizontal"); // already there

    // ADD THIS — stops sliding when key released
    if (Input.GetAxisRaw("Horizontal") == 0)
        playerRB.linearVelocity = new Vector2(0, playerRB.linearVelocity.y);

    playerRB.linearVelocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.linearVelocity.y);
    animator.SetFloat("speed", Mathf.Abs(direction));

    if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
        Flip();
}

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            numberOfJumps = 0;
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, jumpForce);
            numberOfJumps++;
            AudioManager.instance.Play("FirstJump");
        }
        else
        {
            if (numberOfJumps == 1)
            {
                playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, jumpForce);
                numberOfJumps++;
                AudioManager.instance.Play("SecondJump");
            }
        }
    }
}