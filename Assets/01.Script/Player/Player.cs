using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Animator animator;
    private Rigidbody2D rb;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 15f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] SpriteRenderer sprite;

    private float h, v;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 2f;

    public int FacingDir { get; private set; } = 1;
    
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        SetAnimation();
        Move();

        
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if ( h != 0)
        {
            FacingDir = h > 0 ? 1 : -1;
            sprite.flipX = FacingDir == -1;
        }
        Jump();

        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            audioSource.Play();
            StartCoroutine(Dash());
        }
    }

    private void Move()
    {
        rb.linearVelocityX = h * moveSpeed;
    }

    private void SetAnimation()
    {
        

        animator.SetFloat("AxisX", h);
        animator.SetFloat("AxisY", v);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            animator.SetBool("IsGround", true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            animator.SetBool("IsGround", false);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && animator.GetBool("IsGround"))
        {
            animator.SetBool("IsGround", false);

            animator.SetTrigger("Jump");
            rb.AddForceY(jumpPower, ForceMode2D.Impulse);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"), 
            LayerMask.NameToLayer("Monster"), true);

        animator.SetTrigger("Dash");
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.linearVelocity = new Vector2(FacingDir * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Monster"), false);

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    
}
