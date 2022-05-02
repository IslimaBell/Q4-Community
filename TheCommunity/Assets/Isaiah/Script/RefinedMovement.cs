using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool isRunning;
    public int keyCount;
    
    private bool facingRight = true;
    private SpriteRenderer sr;
    
    [Header("Animator")]
    public Animator animator;
    public Animator vingetteAni;

    [Header("Crouching")]
    public bool IsCrouching;
    public BoxCollider2D bc2D;
    public float crouchPercentOfHeightVertical = 0.5f;
    public float crouchPercentOfHeightHorizontal = 2f;
    public float crouchPercentOfHeightOffset = -0.5f;
    private Vector2 standColliderSize;
    private Vector2 standColliderOffset;
    private Vector2 crouchColliderOffset;
    private Vector2 crouchColliderSize;

    public bool ClimbingAllowed { get; set; }

    
    public bool HidingAllowed { get; set; }

    [Header("Hiding Stuff")]
    public bool Hiding;
    public float MaxOpacity = 255f;
    public float CurrentOpacity;
    public float startingOpacity = 0.0f;
    public Image vingetteOpacity;
    public GameObject vingette;
    public GameObject hideIndicator;

    [Header("SoundEffects")]
    public AudioSource jumpSound; 
    public AudioSource normalTheme; 
    public AudioSource chaseTheme; 

    private Vector3 spawnPoint;

    [Header("Music")]
    public bool isBeingChasing;

    [Header("Death")]
    public bool isDead = false;
    public GameObject gameOverPanel;
    



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
        crouchColliderOffset = new Vector2(standColliderOffset.x, standColliderOffset.y + crouchPercentOfHeightOffset); //The y may need to be changed based on size of final design

        runSpeed = moveSpeed * 1.5f;

        spawnPoint = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
              
        //Jump
        if (Input.GetButtonDown("Jump") && IsGrounded() == true && IsCrouching == false && extraJumps > 0 && Hiding == false)
        {
            //Debug.Log("Jump1");
            //jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            extraJumps--;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded() == false && IsCrouching == false && extraJumps > 1 && Hiding == false)
        {
            //Debug.Log("Jump2");
            //jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            extraJumps--;
        }

        //JumpCut
        if (Input.GetButtonUp("Jump") && IsCrouching == false && Hiding == false)
        {
            //Debug.Log("Jump3");
            //jumpSound.Play();
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
            //Debug.Log("Jump4");
            jumpSound.Play();
            jumpStorage = jumpStorageTime;
        }

        if ((jumpStorage > 0) && (coyoteRemember > 0))
        {
            jumpStorage = 0;
            coyoteRemember = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        //Climbing
        if (ClimbingAllowed == true)
        {

            dirY = Input.GetAxisRaw("Vertical") * moveSpeed;

        }

        //Hiding
        if(HidingAllowed == true && Input.GetKeyDown(KeyCode.F))
        {
            Physics2D.IgnoreLayerCollision(9, 10, true);
            sr.sortingOrder = 0;
            Hiding = true;
            Debug.Log(Hiding);
            vingette.SetActive(true);
            hideIndicator.SetActive(false);
            //StartCoroutine(VingetteAnimation());
            //vingetteAni.SetBool("IsHiding", true);

        }
        else if(HidingAllowed == true && Input.GetKeyUp(KeyCode.F))
        {
            Physics2D.IgnoreLayerCollision(9, 10, false);
            sr.sortingOrder = 1;
            Hiding = false;
            vingetteAni.SetBool("IsHiding", false); // Make this a slider for the opacity so people like riley can't interupt the animation           
            //vingette.SetActive(false);
        }
        
        if(Hiding == false)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        else if(Hiding == true)
        {

            horizontal = 0;
            
        }
         
        //Music Change
        if(isBeingChasing == true)
        {
            Debug.Log("ChangingSongs");
            normalTheme.Pause();
            chaseTheme.Play();
        }
        else
        {
            normalTheme.UnPause();
            chaseTheme.Pause();
        }

        //Movement
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
        if (Input.GetKeyDown(KeyCode.LeftControl) && SraminaScript.instance.staminaBar.value >= 10)
        {
            Debug.Log("Run");
            moveSpeed = runSpeed;
            isRunning = true;
                        
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) || SraminaScript.instance.staminaBar.value <= 1)
        {
            moveSpeed = origionalSpeed;
            isRunning = false;
        }
        
        if(Input.GetKey(KeyCode.LeftControl))
        {
                SraminaScript.instance.UseStamina(0.1f);
        }
        Flip();
        Stand();
        Crouch();
        StartCoroutine(VingetteAnimation());

        if(isDead == true)
        {
            gameOverPanel.SetActive(true);
            StartCoroutine(Dead());
        }
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

    //Checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            spawnPoint = transform.position;
            Debug.Log(transform.position);
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

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(1);
        transform.position = spawnPoint;
        isDead = false;
    }

    
    private IEnumerator VingetteAnimation()
    {
        if(Hiding == true)
        {
            while(Input.GetKey(KeyCode.F) && CurrentOpacity < MaxOpacity)
                {
                    CurrentOpacity += 0.00001f;
                    vingetteOpacity.color = new Color(0, 0, 0, CurrentOpacity);
                    yield return CurrentOpacity;
            }
        }

        if (Hiding == false)
        {
            while (CurrentOpacity > 0)
            {
                CurrentOpacity -= 0.0001f;
                vingetteOpacity.color = new Color(0, 0, 0, CurrentOpacity);
                yield return CurrentOpacity;
            }
        }
    }
    
}
  



