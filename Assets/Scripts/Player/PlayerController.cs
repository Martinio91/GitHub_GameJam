using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))] //This ensures there will always be a rigid body as a component
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int numberJumps;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpUpTime;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float wallSlideMultiplier;
    [SerializeField] private float wallJumpDistance;
    [SerializeField] private float maxVelocityMultiplier;
    [SerializeField] private float reactionTime;
    [SerializeField] private bool canQuickDoubleJump; //Adds an extra double jump if the player clicks fast enough
    [SerializeField] private TriggerCount groundCheck;
    [SerializeField] private TriggerCount wallCheck;
    [SerializeField] private UnityEvent OnJump;

    //private KeyCode[] jumpButtons = { KeyCode.W, KeyCode.UpArrow, KeyCode.Space };

    private int numberJumpSinceGrounded;
    private float jumpVelocity;
    private float runVelocity;
    private float gravityUp; //Change in velocity over time
    private float gravityDown;
    private float gravityWall;
    private float wallJumpTime;
    private bool onGround;
    private bool onWall;
    private bool wallJumping;
    private Rigidbody2D rb;
    private Vector2? dirToWall;
    private Vector2 velocity;
    private Transform tr;
    private float timeLeftGround;

    public bool hasKey = false;

    // Awake is called before the game starts
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2();
        tr = transform;
        wallJumping = false;
        timeLeftGround = 0f;
        CalculateConstants();
    }

    // Start is called on the very first frame
    private void Start()
    {
        onGround = (groundCheck.NumberOfObjects > 0);
        onWall = (wallCheck.NumberOfObjects > 0);
    }

    public void HitWall()
    {
        onWall = true;
        numberJumpSinceGrounded = 0;
    }

    public void LeftWall()
    {
        onWall = false;
        numberJumpSinceGrounded = 1;
    }

    public void HitGround()
    {
        onGround = true;
        numberJumpSinceGrounded = 0;
    }

    public void LeftGround()
    {
        onGround = false;
        timeLeftGround = Time.time;
        numberJumpSinceGrounded = 1;
    }

    //Sets all the value of the variables
    private void CalculateConstants()
    {
        jumpVelocity = (2.0f * jumpHeight) / jumpUpTime;
        gravityUp = (jumpVelocity / jumpUpTime) / Mathf.Abs(Physics2D.gravity.y);
        gravityDown = fallGravityMultiplier * gravityUp;
        gravityWall = wallSlideMultiplier * gravityUp;

        float fallTime = Mathf.Sqrt((2.0f * jumpHeight) / Mathf.Abs(Physics2D.gravity.y * gravityDown));

        runVelocity = speed / (fallTime + jumpUpTime);

        wallJumpTime = wallJumpDistance / runVelocity;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        Move();
    }

    //FixedUpdate is similar to Update but it's called based on the number of framerates
    private void FixedUpdate()
    {
        CheckAndSetGravity();
    }

    //Player will jump only if its tounching the ground
    private void Jump()
    {
        if (!onGround)
        {
            //first jump
            if ((Time.time - timeLeftGround) < reactionTime)
            {
                if (!canQuickDoubleJump)
                {
                    return;
                }
                numberJumpSinceGrounded = 0;
            }
            //second jump
            if (numberJumpSinceGrounded >= numberJumps) return;
        }

        //if (((timeLeftGround-Time.time) > reactionTime) && (numberJumpSinceGrounded >= numberJumps)) return;

        numberJumpSinceGrounded++;

        if (onWall)
        {
            dirToWall = wallCheck.GetDirectionToColliders(tr);
            velocity = rb.velocity;
            velocity.y = jumpVelocity;

            if (dirToWall.HasValue)
            {
                velocity.x = -1f * Mathf.Sign(dirToWall.Value.x) * runVelocity;
                StartWallJump();
                //Debug.Log("Set velocity to " + velocity.x);
            }

            rb.velocity = velocity;

        } else
        {
            velocity = rb.velocity;
            velocity.y = jumpVelocity;
            rb.velocity = velocity;
        }
        OnJump?.Invoke();
    }

    void Move()
    {
        if (wallJumping) return;

        velocity = rb.velocity;
        velocity.x = Input.GetAxis("Horizontal") * runVelocity;

        velocity.y = Mathf.Clamp(velocity.y, -1f * maxVelocityMultiplier * jumpVelocity,
            maxVelocityMultiplier * jumpVelocity);
        rb.velocity = velocity;
    }

    private void StartWallJump()
    {
        wallJumping = true;
        Invoke("EndWallJump", wallJumpTime); //This will call the funtion depending on time
    }

    private void EndWallJump()
    {
        wallJumping = false;
    }

    //Trajectory of the player when called (how far in the air they are)
    private void CheckAndSetGravity()
    {
        if (onWall)
        {
            rb.gravityScale = gravityWall;
            return;
        }

        if (rb.velocity.y < 0.0f)
        {
            rb.gravityScale = gravityDown;
        }
        else
        {
            rb.gravityScale = gravityUp;
        }
    }

    public void GotKey()
    {
        hasKey = true;
    }
}
