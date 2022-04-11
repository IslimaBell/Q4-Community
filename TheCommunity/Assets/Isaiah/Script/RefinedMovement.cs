using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinedMovement : MonoBehaviour
{


    float coyoteRemember = 0;[Header("Jump")]
    [SerializeField]
    float coyoteTime = 0.25f;

    float jumpStorage = 0;
    [SerializeField]
    float jumpStorageTime = 0.25f;

    private int extraJumps;
    [SerializeField]
    private int extraJumpsValue;

    [SerializeField]
    private float jumpCut = 0.5f;
    public LayerMask ground;
    [SerializeField]
    private float jumpPower = 1f;

    private float horizontal;

    [Header("Move")]
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float origionalSpeed = 1f;
    [SerializeField]
    private float dirY;
    Rigidbody2D rb;

    private float runSpeed;
    
    private bool facingRight = true;
    private SpriteRenderer sr;
    
    [Header("Animator")]
    public Animator animator;

    [Header("Crouching")]
    public bool IsCrouching;
    public BoxCollider2D bc2D;
    public float crouchPercentOfHeightVertical = 0.5f;
    public float crouchPercentOfHeightHorizontal = 2;
    public float crouchPercentOfHeightOffset = -0.25f;
    private Vector2 standColliderSize;
    private Vector2 standColliderOffset;
    private Vector2 crouchColliderOffset;
    private Vector2 crouchColliderSize;

    public bool ClimbingAllowed { get; set; }

    public bool HidingAllowed { get; set; }
    public bool Hiding;
  


    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc2D = GetComponent<BoxCollider2D>();

        standColliderSize = bc2D.size;
        standColliderOffset = bc2D.offset;
        crouchColliderSize = new Vector2(standColliderSize.x * crouchPercentOfHeightHorizontal, standColliderSize.y * crouchPercentOfHeightVertical); // The x may need to be changed based on size of final design
        crouchColliderOffset = new Vector2(standColliderOffset.x, -0.25f); //The y may need to be changed based on size of final design

        runSpeed = moveSpeed * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && IsGrounded() == true && IsCrouching == false && extraJumps > 0 && Hiding == false) // Jump
        {
            Debug.Log("Jump1");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            extraJumps--;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded() == false && IsCrouching == false && extraJumps > 1 && Hiding == false)
        {
            Debug.Log("Jump2");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            extraJumps--;
        }

        if (Input.GetButtonUp("Jump") && IsCrouching == false && Hiding == false) //Jump cut
        {
            Debug.Log("Jump3");
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCut);
            }
        }

        if (IsGrounded() == true)
        {
            extraJumps = extraJumpsValue;
        }

        coyoteRemember -= Time.deltaTime;
        if (IsGrounded())
        {
            coyoteRemember = coyoteTime;
        }

        jumpStorage -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && IsCrouching == false && Hiding == false)
        {
            Debug.Log("Jump4");
            jumpStorage = jumpStorageTime;
        }

        if ((jumpStorage > 0) && (coyoteRemember > 0))
        {
            jumpStorage = 0;
            coyoteRemember = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (ClimbingAllowed == true) // Climbing
        {

            dirY = Input.GetAxisRaw("Vertical") * moveSpeed;

        }

        if(HidingAllowed == true && Input.GetKeyDown(KeyCode.F)) //Hiding
        {
            Physics2D.IgnoreLayerCollision(9, 10, true);
            sr.sortingOrder = 0;
            Hiding = true;
            Debug.Log(Hiding);
        }
        else if(HidingAllowed == true && Input.GetKeyUp(KeyCode.F))
        {
            Physics2D.IgnoreLayerCollision(9, 10, false);
            sr.sortingOrder = 1;
            Hiding = false;
        }
        

        if(Hiding == false)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        else if(Hiding == true)
        {

            horizontal = 0;
            
        }
         //Movement
        //Debug.Log(horizontal);
        //Move right
        if (Input.GetAxis("Horizontal") > 0 && Hiding == false)
        {
            //sr.flipX = false;
            rb.AddForce(new Vector2(moveSpeed, 0));
            
        }

        //Move left
        if (Input.GetAxis("Horizontal") < 0 && Hiding == false)
        {
            //sr.flipX = true;
            rb.AddForce(new Vector2(-moveSpeed, 0));
            
        }

        //Running
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Run");
            moveSpeed = runSpeed;
            while (Input.GetKeyDown(KeyCode.LeftControl))
            {
                SraminaScript.instance.UseStamina(15);
            }
            
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            moveSpeed = origionalSpeed;
        }
        Flip();
        Stand();
        Crouch();

    }


    private void Flip()
    {
        if (facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            facingRight = !facingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y); //Baisc Movement

        if (ClimbingAllowed) //Climbing
        {
            //animator.SetBool("IsClimbing", true);
            rb.gravityScale = 0;
            rb.velocity = new Vector2(horizontal * moveSpeed, dirY);
        }
        else
        {
            //animator.SetBool("IsClimbing", false);
            rb.gravityScale = 1;
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }

  

    }

    public bool IsGrounded()
    {
        bool grounded = Physics2D.BoxCast(transform.position + new Vector3(0f, 0f, 0f), new Vector3(0.1f, 0.7f, 0f), 0, Vector2.down, 0.7f, ground);

        return grounded;
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Hiding == false)
        {
            IsCrouching = true;
            moveSpeed = moveSpeed * 0.5f;
            bc2D.size = crouchColliderSize;
            bc2D.offset = crouchColliderOffset;
        }
    }

    private void Stand()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) && Hiding == false)
        {
            IsCrouching = false;
            moveSpeed = moveSpeed * 2f;
            bc2D.size = standColliderSize;
            bc2D.offset = standColliderOffset;
        }
    }
}
  



