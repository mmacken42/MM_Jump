using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private float moveSpeedStart;
    public float speedMultiplier = 1.05f;
    public float speedIncreaseMilestone = 50f;
    private float speedIncreaseMilestoneStart;
    private float speedMilestoneCountStart;
    private float speedMilestoneCount;

    public float jumpForce = 12.0f;
    public float jumpTime = 0.25f;
    private float jumpTimeCounter;

    private Rigidbody2D rb;


    public bool grounded;
    public LayerMask walkable;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.1f;

    //private Collider2D col;

    private Animator anim;

    public GameManager manager;

    private bool stoppedJumping;
    private bool canDoubleJump;

    public AudioSource jumpSound;
    public AudioSource deathSound;
    

	void Start ()
    {
        

        rb = GetComponent<Rigidbody2D>();

        //col = GetComponent<Collider2D>();

        anim = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStart = moveSpeed;
        speedMilestoneCountStart = speedMilestoneCount;
        speedIncreaseMilestoneStart = speedIncreaseMilestone;
        stoppedJumping = true;
        canDoubleJump = true;
    }
	
	void Update ()
    {
        //detect grounded or not
        //grounded = Physics2D.IsTouchingLayers(col, walkable);
        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, walkable);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone *= speedMultiplier;

            moveSpeed *= speedMultiplier;
        }

        //horizontal movement = endless running
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        //vertical movement for jump key
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //only jump when grounded
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }

            if (!grounded && canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                stoppedJumping = false;
                canDoubleJump = false;
                jumpTimeCounter = jumpTime;
                jumpSound.Play();
            }
            
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
            
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

        anim.SetFloat("HorizontalSpeed", rb.velocity.x);
        anim.SetBool("Grounded", grounded);



	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Death")
        {
            deathSound.Play();

            manager.RestartGame();

            moveSpeed = moveSpeedStart;
            speedMilestoneCount = speedMilestoneCountStart;
            speedIncreaseMilestone = speedIncreaseMilestoneStart;
            
        }
    }
}
