// PlayerController.cs

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] ParticleSystem SplatPS;

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    [Header("Running")]

    public float TopSpeed = 100f;
    [SerializeField] float Acceleration;
    [SerializeField] float Friction;

    private float _horizontalInput;

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    [Header("Jumping")]

    [SerializeField] LayerMask GroundLayer;
    [SerializeField] BoxCollider2D PlayerCollider;
    [SerializeField] float JumpPower = 10f;
    [SerializeField, Range(0, 1)] float JumpCutoff;
    [SerializeField] float fallGravity;
    [SerializeField] float JumpBuffer;
    [SerializeField] float CoyoteBuffer;

    private bool _isGrounded;
    private bool _jumpCutOff;
    private bool _isJumping;

    // coyote time and jump buffer timer
    private float _lastGroundedTimer;
    private float _lastJumpedTimer;

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    [Header("Wall Slide")]
    [SerializeField] LayerMask WallLayer;
    [SerializeField] float WallSlideSpeed;
    [SerializeField] float CoyoteWallBuffer;

    private bool _isWalled;
    private float _lastWalledTimer;

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    [Header("Other")]

    [SerializeField] float MaxFallSpeed;

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _lastJumpedTimer = JumpBuffer;
            _jumpCutOff = false;
        }
        else if (Input.GetButtonUp("Jump") && (_lastJumpedTimer > 0 || _isJumping))
            _jumpCutOff = true;
    }

    private void FixedUpdate()
    {
        _isGrounded = IsTouchingGround();
        _isWalled = !_isGrounded && _horizontalInput != 0 && IsTouchingWall();

        AddMovement();

        // Jumping with coyote time and jump buffer
        if (_lastJumpedTimer > 0 && _lastGroundedTimer > 0 && !_isJumping)
        {
            Jump();
            CreateDust();
        }
        else if (_isGrounded)
            _isJumping = false;

        if (_jumpCutOff)
            CutOffJump();

        // Change falling gravity
        if (rb2D.velocity.y < 0)
            rb2D.gravityScale = fallGravity;
        else
            rb2D.gravityScale = 1;

        // Clamp falling speed based on contact surface
        if (_isWalled)
            ClampFallSpeed(WallSlideSpeed);
        else
            ClampFallSpeed(MaxFallSpeed);

        UpdateTimers();
    }



    private void UpdateTimers()
    {
        _lastGroundedTimer -= Time.fixedDeltaTime;
        _lastJumpedTimer -= Time.fixedDeltaTime;
        _lastWalledTimer -= Time.fixedDeltaTime;
    }

    private void AddMovement()
    {
        float targetSpeed = _horizontalInput * TopSpeed;
        if (targetSpeed != 0) // accelerate and change direction
        {
            float speedDifference = targetSpeed - rb2D.velocity.x;
            float movement = speedDifference * Acceleration;
            rb2D.AddForce(movement * Vector2.right);
        }
        else // deccelerate to stop
        {
            float amount = Mathf.Min(Mathf.Abs(rb2D.velocity.x), Mathf.Abs(Friction));
            amount *= Mathf.Sign(rb2D.velocity.x);
            rb2D.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    public bool IsTouchingGround() // Checks if player is touching a collider on ground layer  
    {
        if (Physics2D.BoxCast(
            PlayerCollider.bounds.center,
            PlayerCollider.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            GroundLayer))
        {
            _lastGroundedTimer = CoyoteBuffer;
            return true;
        }
        return false;
    }

    public bool IsTouchingWall() // Checks if player is pushing into a collider on wall layer  
    {
        if (_horizontalInput != 0 && Physics2D.BoxCast(PlayerCollider.bounds.center,
            PlayerCollider.bounds.size,
            0f,
            Vector2.right * Mathf.Sign(_horizontalInput),
            0.1f,
            GroundLayer))
        {
            _lastWalledTimer = CoyoteWallBuffer;
            return true;
        }
        return false;
    }

    private void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, JumpPower);
        _lastJumpedTimer = 0;
        _isJumping = true;
    }

    private void CutOffJump() // Adds downward force if jumping and jump button is released  
    {
        if (rb2D.velocity.y > 0)
        {
            rb2D.AddForce(rb2D.velocity.y * JumpCutoff * Vector2.down, ForceMode2D.Impulse);
            _jumpCutOff = false;
        }
    }

    private void ClampFallSpeed(float speed)
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Max(rb2D.velocity.y, -speed));
    }

    private void CreateDust()
    {
        SplatPS.Play();
    }
}
