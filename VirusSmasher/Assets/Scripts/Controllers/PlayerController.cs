using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

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
    public bool _canDash;

    private float _horizontal;
    [HideInInspector] public Rigidbody2D _rb;

    private bool _isJumping;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        lastDirection = 1;
    }

    void Update()
    {
        Horizontal = InputManager.Move.ReadValue<Vector2>().x;

        if(InputManager.Jump.triggered && IsGrounded())
            _isJumping = true;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(Horizontal * speed,_rb.velocity.y);

        if( _isJumping )
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _isJumping = false;
        }

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
        if (IsGrounded())
        {
            _canDash = true;
            Debug.Log("grounded");
        }
            
    }


    //returns true if player is ontop of an object with the ground layer
    public bool IsGrounded() => Physics2D.OverlapCircle(_groundCheck.position, 1f, LayerMask.GetMask("Ground")); 
}
