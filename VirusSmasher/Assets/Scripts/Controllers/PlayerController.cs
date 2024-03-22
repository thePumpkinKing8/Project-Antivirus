using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;

    [HideInInspector] public float lastDirection = 1;

    
    public float Horizontal 
    {
        get
        {
            return _horizontal;
        }

        //this is messy and may no longer be needed. clean up after prototype
        private set
        {
            if (value != 0)
            {
                _horizontal = value;
                if(lastDirection != value)
                {
                    lastDirection = value;
                    FlipPlayer();
                }
            }
            else
                _horizontal = value;
        } 
    }

    private float _currentHealth;

    [HideInInspector] public bool grounded = true;
    private float _horizontal;

    public PlayerSettings settings;
    [HideInInspector] public Rigidbody2D _rb;
    [HideInInspector] public InputController inputController;
    [HideInInspector] public Dash dashPower;

    //state machine variables
    #region StateMachine
    private BaseState _currentState;

    //states
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public WalkingState walkingState;
    [HideInInspector] public JumpState jumpState;
    [HideInInspector] public FallingState fallingState;
    [HideInInspector] public DashState dashState;
    [HideInInspector] public HitState hitState;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();

        //set player health
        _currentHealth = settings.maxHealth;

        //powers
        dashPower = GetComponent<Dash>();

        lastDirection = 1;

        // set up player states
        idleState = new IdleState(this);
        walkingState = new WalkingState(this);
        fallingState = new FallingState(this);
        jumpState = new JumpState(this);
        dashState = new DashState(this);
        hitState = new HitState(this);

        ChangeState(idleState);
        
    }

    void Update()
    {
        Horizontal = InputManager.Move.ReadValue<Vector2>().x;
        _currentState.UpdateState();
        _currentState.HandleInput();

        //test input
        if (Input.GetKeyDown(KeyCode.V))
            OnHit(10, new Vector2(-1, 0));
    }

    public void ChangeState(BaseState state)
    {
       _currentState?.ExitState();
        _currentState = state;
        _currentState?.EnterState();
    }

    public BaseState GetCurrentState() => _currentState;

    private void FixedUpdate()
    {

    }

    private void FlipPlayer()
    {
        var size = transform.localScale;

        transform.localScale = new Vector3(size.x * -1,size.y,size.z);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
            Debug.Log("trigger");
        }          
    }

    public void OnHit(float damage, Vector2 direction)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Die();
        else
        {
            ChangeState(hitState);
            hitState.direction = direction;
        }

    }

    private void Die()
    {
        //player dies
    }

    //returns true if player is ontop of an object with the ground layer
    public bool IsGrounded() => Physics2D.OverlapCircle(_groundCheck.position, settings.groundCheckRadius, settings.groundLayerMask);

    public bool CanDash() => dashPower.SetDash();

    /// <summary>
    /// Temporary function to reset dash cooldown until an event system is implimented
    /// </summary>
    public void TempCoolDownReset() => dashPower.timer = 0;



}
