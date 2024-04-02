using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Patrol()
    {
        base.Patrol();

        _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _speed *= -1;
    }
}



