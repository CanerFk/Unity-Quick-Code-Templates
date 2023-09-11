using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dirX = 0f;
    private float coyoteTime = 0.1f; // Coyote Time süresi (saniye cinsinden)
    private float coyoteTimeCounter = 0f;
    private bool doubleJumpAllowed = true;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, fall };

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded() || coyoteTimeCounter > Time.time)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                AudioManager.Instance.Play("Jump");
                coyoteTimeCounter = 0f;
                doubleJumpAllowed = true;
            }
            else if(doubleJumpAllowed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                AudioManager.Instance.Play("DoubleJump");
                doubleJumpAllowed = false;
            }
        }

        if (isGrounded())
        {
            coyoteTimeCounter = Time.time + coyoteTime;
        }

        AnimationUpdate();
    }

    void AnimationUpdate()
    {
        MovementState state;
        if (dirX > 0 || dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = dirX > 0 ? false : true;
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
