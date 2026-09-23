using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference dash;

    [SerializeField] private InputActionReference vertical;

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform leftfoot, rightfoot, lefthand, righthand;

    [SerializeField] private float raycastDistance = 0.1f;


    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float dashForce = 1f;
    [SerializeField] private float verticalSpeed = 1f;
   [SerializeField] private AudioClip[] jumpSFXs;

    [SerializeField] private ParticleSystem jumpParticleSys;



    public bool canMove = true;

    public bool canDash;

    private bool isGrounded;



    private float moveDirection;

    private float verticalDirection;

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
        dash.action.started += Dash;
        canDash = true;
       



    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
        dash.action.started -= Dash;
    }



    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<float>();

        verticalDirection = vertical.action.ReadValue<float>();

        anim.SetFloat("MoveSpeed", MathF.Abs(rb.linearVelocity.x));
        anim.SetFloat("VerticalMoveSpeed", MathF.Abs(rb.linearVelocity.y));
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


        isGrounded = CheckIsGrounded();
          if (isGrounded == true)
        {
            canDash = true;
        } 
    }


    private void FlipSprite(bool direction)
    {
        sr.flipX = direction;
    }

    private void Jump(InputAction.CallbackContext context)
    {
      
        if (CheckIsGrounded() == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce));
         
            
            jumpParticleSys.Play();
            int randomjumpSFX = UnityEngine.Random.Range(0, jumpSFXs.Length);
            audiosrc.PlayOneShot(jumpSFXs[randomjumpSFX]);
        }

       
        CheckIsWall(); /*== true)*/
        

    }
    public void Dash(InputAction.CallbackContext context)
    {

     

        if (canDash == false) return;


       
        canDash = false;
        canMove = false;
        

        Invoke("CanMoveAgain", 0.25f);
        
        if (isGrounded == false)
        {
            canDash = false;
        } else if (isGrounded == true)
        {
            Invoke("CanDashAgain", 0.75f);
        }

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.linearVelocity = new Vector2(moveDirection * dashForce, rb.linearVelocity.y);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalDirection * dashForce / 1.4f);

        


    }


    
    private void CheckIsWall()
    {
        RaycastHit2D lefthandhit = Physics2D.Raycast(lefthand.position, Vector2.left, raycastDistance, whatIsGround);
        RaycastHit2D righthandhit = Physics2D.Raycast(righthand.position, Vector2.right, raycastDistance, whatIsGround);

        

        if (lefthandhit.collider != null && lefthandhit)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            canMove = false;
            rb.AddForce(new Vector2(400, jumpForce ));
            Invoke("CanMoveAgain", 0.25f);

        }


        if (righthandhit.collider != null && righthandhit)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            canMove = false;
            rb.AddForce(new Vector2(-400, jumpForce ));
            Invoke("CanMoveAgain", 0.25f);

        }


        if (righthandhit.collider != null && righthandhit && lefthandhit.collider != null && lefthandhit)
        {
            rb.AddForce(new Vector2(0, -jumpForce));
        }


    }
   
    public bool CheckIsGrounded()
    {
        RaycastHit2D lefthit = Physics2D.Raycast(leftfoot.position, Vector2.down, raycastDistance, whatIsGround);
        RaycastHit2D righthit = Physics2D.Raycast(rightfoot.position, Vector2.down, raycastDistance, whatIsGround);
       


        Debug.DrawRay(leftfoot.position, Vector2.down * raycastDistance, Color.red, 0.25f);
        
        Debug.DrawRay(leftfoot.position, Vector2.down * raycastDistance, Color.red, 0.25f);
       
        if (lefthit.collider != null && lefthit || righthit.collider != null && righthit )
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

    private void CanDashAgain()
    {
        canDash = true;
    }

   


}
