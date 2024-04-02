using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;

    [HideInInspector] public float lastDirection = 1;


   [HideInInspector] public Healthbar healthbar;


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

    //GameEvents
    #region Events


    [HideInInspector] public GameEvent jumpEvent;
    [HideInInspector] public GameEvent hurtEvent;
   
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
        #region StateSetUp
        idleState = new IdleState(this);
        walkingState = new WalkingState(this);
        fallingState = new FallingState(this);
        jumpState = new JumpState(this);
        dashState = new DashState(this);
        hitState = new HitState(this);
        #endregion
        ChangeState(idleState);

        #region EventSetUp
        jumpEvent = EventSetUp("JumpEvent");
        hurtEvent = EventSetUp("HurtEvent");
        #endregion
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
        ChangeHealth(-damage);

        if (_currentHealth <= 0)
            Die();
        else
        {
            hitState.direction = direction;
            ChangeState(hitState);
        }
    }

    public void ChangeHealth(float health)
    {
        _currentHealth += health;
        healthbar.ChangeHealthFill(_currentHealth);  //Adjusts healthbar based on players health
    }
    private void Die()
    {
        //player dies
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

    private GameEvent EventSetUp(string name)
    {
        foreach(GameEvent gameEvent in Resources.LoadAll<GameEvent>("Events"))
        {
            if(gameEvent.name == name)
                return gameEvent;
        }
        Debug.Log($"no Event with the name {name} found");
        return null;
    }



}
