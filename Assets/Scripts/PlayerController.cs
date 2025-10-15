using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private GameManager gameManager;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private float moveInput;
    private bool isDead = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    [System.Obsolete]
    private void Update()
    {
        if (isDead)
        {
            if (rb.linearVelocity != Vector2.zero)
            {
                rb.linearVelocity = Vector2.zero;
            }
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (moveInput > 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (moveInput < 0)
            GetComponent<SpriteRenderer>().flipX = false;

        UpdateAnimationStates();
    }
    private void UpdateAnimationStates()
    {
        if (!isGrounded)
        {
            anim.SetInteger("playerState", 2);
        }
        else if (Mathf.Abs(moveInput) > 0.01f)
        {
            anim.SetInteger("playerState", 1);
        }
        else
        {
            anim.SetInteger("playerState", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        anim.SetInteger("playerState", 3);

        if (gameManager != null)
        {
            gameManager.ReloadLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            if (gameManager != null)
            {
                gameManager.coins += 1;
            }
            Destroy(other.gameObject);
        }
    }

}
