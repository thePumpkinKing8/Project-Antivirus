using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private Transform _groundCheck;

    private float _horizontal;
    private Rigidbody2D _rb;

    private bool _isJumping;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded())
            _isJumping = true;

    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speed,_rb.velocity.y);

        if( _isJumping )
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _isJumping = false;
        }
    }

    //returns true if player is ontop of an object with the ground layer
    public bool IsGrounded() => Physics2D.OverlapCircle(_groundCheck.position, 1f, LayerMask.GetMask("Ground")); 
}
