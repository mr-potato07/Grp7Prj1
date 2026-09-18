using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform leftfoot, rightfoot;

    [SerializeField] private float raycastDistance = 0.1f;


    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private AudioClip[] jumpSFXs;

    [SerializeField] private ParticleSystem jumpParticleSys;



    bool canMove = true;
    private float moveDirection;

    private AudioSource audiosrc;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audiosrc = GetComponent<AudioSource>();
        jump.action.started += Jump;

    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
    }



    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<float>();

        anim.SetFloat("MoveSpeed", MathF.Abs(rb.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", rb.linearVelocity.y);
        anim.SetBool("IsGrounded", CheckIsGrounded());




        if (moveDirection < 0f)
        {
            FlipSprite(true);
        }
        if (moveDirection > 0f)
        {
            FlipSprite(false);
        }

    }

    private void FixedUpdate()
    {
        if (!canMove) { return; }
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
    }


    private void FlipSprite(bool direction)
    {
        sr.flipX = direction;
    }

    private void Jump(InputAction.CallbackContext context)
    {

        if (CheckIsGrounded() == true)
        {

            rb.AddForce(new Vector2(0, jumpForce));
            jumpParticleSys.Play();
            int randomjumpSFX = UnityEngine.Random.Range(0, jumpSFXs.Length);
            audiosrc.PlayOneShot(jumpSFXs[randomjumpSFX]);
        }


    }

    private bool CheckIsGrounded()
    {
        RaycastHit2D lefthit = Physics2D.Raycast(leftfoot.position, Vector2.down, raycastDistance, whatIsGround);
        RaycastHit2D righthit = Physics2D.Raycast(rightfoot.position, Vector2.down, raycastDistance, whatIsGround);
        Debug.DrawRay(leftfoot.position, Vector2.down * raycastDistance, Color.red, 0.25f);
        Debug.DrawRay(rightfoot.position, Vector2.down * raycastDistance, Color.red, 0.25f);
        if (lefthit.collider != null && lefthit || righthit.collider != null && righthit)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    public void TakeKnockBack(float knockbackforce, float upwardsforce)
    {
        canMove = false;
        rb.AddForce(new Vector2(knockbackforce, upwardsforce));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }
}
