using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Moviment")]
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    public float xAxis, yAxis;
    public bool noInput;

    [Header("Stand")]
    public Animator standAnim;
    public GameObject stand;

    [Header("Shoot")]
    [SerializeField] GameObject projectile;
    public Transform StandFirePoint;
    public Transform upFirePoint;

    [Header("Ball")]
    public Animator ballAnim;
    public GameObject ball, bomb;
    bool ballMode;

    [Header("Dash Trail")]
    public SpriteRenderer sr;
    public SpriteRenderer dashTrailImage;
    public float dashTrailLifetime, timeBetweenTrail;
    public Color dashTrailColor;
    [HideInInspector] public float dashTrailCounter;

    [Header("Abilities")]
    public bool canDoubleJump;
    public bool canDash;
    public bool canChangeMode, canDropBomb;

    [Header("Checks")]
    public string STATE;
    public string currentAnimation;
    public bool isGrounded;
    public Transform groundPoint;
    public LayerMask whatIsGround;
    public float coyoteCounter;

    //State Machine
    public Abstract currentState;
    public Player_IdleState idleState = new Player_IdleState();
    public Player_RunState runState = new Player_RunState();
    public Player_JumpState jumpState = new Player_JumpState();
    public Player_ShootState shootState = new Player_ShootState();
    public Player_ShootUpState shootUpState = new Player_ShootUpState();
    public Player_DashState dashState = new Player_DashState();
    public Player_BallMode ballState = new Player_BallMode();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        noInput = false;

        currentState = idleState;
        currentState.EnterState(this);
    }
    void Update()
    {
        //Debug
        STATE = currentState.ToString();

        //Inputs & Checks
        if (currentState != dashState)
        {
            if (!noInput)
            {
                xAxis = Input.GetAxisRaw("Horizontal");
                yAxis = Input.GetAxisRaw("Vertical");
            }
        }

        //StateMachine Update();
        currentState.LogicsUpdate(this);

        isGrounded = Physics2D.OverlapCircle(groundPoint.transform.position, 0.1f, whatIsGround);

        //Coyote
        if (isGrounded)
        {
            coyoteCounter = 0.2f;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
        
        //Flip Sprite
        if(xAxis > 0)
        {
            transform.localScale = Vector3.one;
        }
        if (xAxis < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }
    public void SwitchState(Abstract newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    public void ChangeAnimationState(string newAnimation)
    {
        //Define Animator
        Animator animator;
        if(currentState == ballState)
        {
            animator = ballAnim;
        }
        else
        {
            animator = standAnim;
        }

        //Swipe animations
        if (currentAnimation == newAnimation)
        {
            return;
        }
        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    public void Shot(Transform firePoint)
    {
        if (ballMode)
        {
            firePoint = transform;
            Instantiate(bomb, transform.position, transform.rotation);
            AudioController.instance.PlaySfxAdjusted(4);
        }
        else
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            AudioController.instance.PlaySfxAdjusted(4);
        }
    }
    public void DashTrail()
    {
        if (currentState == dashState)
        {
            AudioController.instance.PlaySfxAdjusted(7);
            SpriteRenderer image = Instantiate(dashTrailImage, transform.position, transform.rotation);
            image.sprite = sr.sprite;
            image.transform.localScale = transform.localScale;
            image.color = dashTrailColor;

            Destroy(image.gameObject, dashTrailLifetime);
            dashTrailCounter = timeBetweenTrail;
        }
    }
    public void ChangeMode()
    {
        if (!ballMode)
        {
            stand.gameObject.SetActive(false);
            ball.gameObject.SetActive(true);
            ballMode = true;
            AudioController.instance.PlaySfxAdjusted(5);
        }
        else
        {
            ball.gameObject.SetActive(false);
            stand.gameObject.SetActive(true);
            ballMode = false;
            AudioController.instance.PlaySfxAdjusted(6);
        }
    }
}
