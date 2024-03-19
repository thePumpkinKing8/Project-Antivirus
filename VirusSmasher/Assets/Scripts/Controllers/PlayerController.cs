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

        private set
        {
            if (value != 0)
            {
                _horizontal = value;
                lastDirection = value;
                FlipPlayer();
            }
            else
                _horizontal = value;
        } 
    }
    
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
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();

        //powers
        dashPower = GetComponent<Dash>();

        lastDirection = 1;

        // set up player states
        idleState = new IdleState(this);
        walkingState = new WalkingState(this);
        fallingState = new FallingState(this);
        jumpState = new JumpState(this);
        dashState = new DashState(this);

        ChangeState(idleState);
    }

    void Update()
    {
        Horizontal = InputManager.Move.ReadValue<Vector2>().x;
        _currentState.UpdateState();
        _currentState.HandleInput();
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
        if (lastDirection == -1)
            transform.localScale = new Vector3(-1,1,1);
        else if (lastDirection == 1)
            transform.localScale = Vector3.one;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
            Debug.Log("trigger");
        }          
    }


    //returns true if player is ontop of an object with the ground layer
    public bool IsGrounded() => Physics2D.OverlapCircle(_groundCheck.position, settings.groundCheckRadius, settings.groundLayerMask);

    public bool CanDash() => dashPower.SetDash();

    /// <summary>
    /// Temporary function to reset dash cooldown until an event system is implimented
    /// </summary>
    public void TempCoolDownReset() => dashPower.timer = 0;
}
