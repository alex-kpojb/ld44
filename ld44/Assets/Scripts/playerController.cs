using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public ParticleSystem particleDash;
    public ParticleSystem particleBlood;
    public GameStateSO stateSO;

    public Transform topleftGrounCheck;
    public Transform bottomRightGroundCheck;

    public bool inshoprange= false;
    Rigidbody2D rb;
    Collider2D collider2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    TrailRenderer trailRenderer;
    public bool freeze;

    public AudioClip jump;
    public AudioClip grounded;
    /*
    float jumpForce = 800f;
    float maxJumps = 2;
    
    float walkForce = 0.2f;

    float dashMax = 2;
    
    float dashCoolDown = 0.2f;
    float dashSpeed = 25f;
    */
    float avialableJumps;
    float dashAvialable;

    bool isDashing = false;
    bool isGrounded = false;

    int playerLayer;
    int groundLayer;

    LayerMask groundMask;

    bool JCoolDown = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();

        avialableJumps = stateSO.maxJumps;
        dashAvialable = stateSO.dashMax;

        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
        groundMask = LayerMask.GetMask("Ground");
    }


    bool lastGrounded;
    private void FixedUpdate()
    {
        //Debug.Log("lastgrounded: " + lastGrounded);
        //Debug.Log("isgrounded: " + isGrounded);
        
       

        if (isGrounded) 
        {
            animator.SetBool("jump", false);
            // animator.SetBool("jump", false);
            avialableJumps = stateSO.maxJumps;
            dashAvialable = stateSO.dashMax;
        }


        if (Input.GetKeyDown(KeyCode.Space) && !freeze /*&& JCoolDown*/ && !inshoprange)
        {
            if (avialableJumps > 0)
            {
                //animator.SetBool("jump", true);
                animator.SetTrigger("jumpTrigger");
                StartCoroutine(jumpCoolDown());
                avialableJumps--;
                rb.AddForce(Vector2.up * stateSO.jumpForce);
                particleDash.Emit(25);
            }
        }

        //rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * walkForce, rb.velocity.y));
        if (!freeze)
            transform.Translate(Input.GetAxisRaw("Horizontal") * stateSO.walkForce, 0, 0);

        //print($"{rb.velocity.magnitude}");
        if (!freeze)
            animator.SetBool("isWalk", (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 & isGrounded) ? true : false);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !freeze)
        {
            if (dashAvialable > 0)
            {
                dashAvialable--;
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dashDirection = mousePosition - transform.position;


                if (!isDashing)
                    StartCoroutine(Dash(dashDirection.normalized));

            }
        }


    }

    void Update()
    {
        animator.SetBool("grounded", isGrounded);
        checkGrounded();
        if (!freeze && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            spriteRenderer.flipX = (Input.GetAxis("Horizontal") < 0) ? true : false;
        }
    }

    IEnumerator Dash(Vector2 dashDirection)
    {
        animator.SetTrigger("Dash");
        trailRenderer.emitting = true;
        //animator.SetBool("isDashStart", true);
        isDashing = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Visibility"), true);
        Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);


        var cachedGravity = rb.gravityScale;

        rb.gravityScale = 0;

        animator.SetBool("isDashEnd", false);
        this.gameObject.tag = "Dash";
        particleDash.Emit(25);

        rb.velocity = dashDirection * stateSO.dashSpeed;
        yield return new WaitForSeconds(stateSO.dashTime);

        //animator.SetBool("isDashStart", false);
        animator.SetBool("isDashEnd", true);
        if (!isGrounded)
            animator.SetBool("isJumpMiddle", true);

        rb.velocity = Vector2.zero;

        rb.gravityScale = cachedGravity;
        this.gameObject.tag = "Player";

        trailRenderer.emitting = false;
        isDashing = false;

        Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Visibility"), false);


        yield return null;
    }
    public float Yoffset;

    private void OnDrawGizmos() {
        Gizmos.DrawLine(topleftGrounCheck.position, bottomRightGroundCheck.position);
    }

    void checkGrounded()
    {
        isGrounded = Physics2D.OverlapArea(topleftGrounCheck.position, bottomRightGroundCheck.position, groundMask);
    }

    IEnumerator jumpCoolDown()
    {
        JCoolDown = false;
        yield return new WaitForSeconds(0.2f);
        
            JCoolDown= true;
        
    }
    
}
