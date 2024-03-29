using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;

    [HideInInspector] public float lastDirection = 1;


    public Image healthbar;


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
        if (GameManager.Instance.player == null)
            GameManager.Instance.player = this;

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

    }

    public void ChangeState(BaseState state)
    {
       _currentState?.ExitState();
        _currentState = state;
        _currentState?.EnterState();
    }

    public BaseState GetCurrentState() => _currentState;


    private void FlipPlayer()
    {
        var size = transform.localScale;
        Vector3 direction = GetPlayerDirection();


        transform.localScale = new Vector3(direction.x * Mathf.Abs(size.x),size.y,size.z);

    
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }     

        //prevents player from dashing through objects
        if(_currentState == dashState)
        {
            ChangeState(IsGrounded() ? walkingState : fallingState);
            _rb.velocity = Vector3.zero;
            transform.position = dashState.storedPosition;
        }
    }

    public void OnHit(float damage, Vector2 direction)
    {
        _currentHealth -= damage;

        healthbar.fillAmount = _currentHealth / settings.maxHealth;  //Adjusts healthbar based on players health

        if (_currentHealth <= 0)
            Die();
        else
        {
            hitState.direction = direction;
            ChangeState(hitState);
        }

    }

    private void Die()
    {
        //player dies
    }


    public void OnHeal(float heal, Vector2 direction)
    {
        _currentHealth += heal;

        healthbar.fillAmount = _currentHealth / settings.maxHealth;   //Adjusts healthbar based on players health

        _currentHealth = Mathf.Clamp(_currentHealth, 0, settings.maxHealth);   //Prevents overhealing past max health

    }


    private Vector3 GetPlayerDirection()
    {
        return inputController.MoveInput.x switch
        {
            > 0 => Vector3.right,
            < 0 => Vector3.left,
            _ => new Vector3(Mathf.Round(lastDirection),0,0),
        };
    }


    //returns true if player is ontop of an object with the ground layer
    public bool IsGrounded() => Physics2D.OverlapCircle(_groundCheck.position, settings.groundCheckRadius, settings.groundLayerMask);

    public bool CanDash() => dashPower.SetDash();

    /// <summary>
    /// Temporary function to reset dash cooldown until an event system is implimented
    /// </summary>
    public void TempCoolDownReset() => dashPower.timer = 0;



}
