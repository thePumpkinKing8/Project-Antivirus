using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _state = EnemyState.Patrol;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if( _state == EnemyState.Patrol )
        {
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x - (Mathf.Sign(_speed) * 1.5f), transform.position.y), Vector2.down, 1f, ~LayerMask.GetMask("Enemy"));

            if (ray.point == Vector2.zero)
            {
                Debug.Log(transform.position.x + (Mathf.Sign(_speed) * 1.5f));
                _speed *= -1;
                _rb.velocity = Vector2.zero;
                _state = EnemyState.Wait;
            }
        }
    }

    public override void Patrol()
    {
        base.Patrol();
        _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
       
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //switch statment to determin what type of object this collided with
        string cLayer = LayerMask.LayerToName(collision.gameObject.layer);
        switch (cLayer)
        {
            case "Player":
                HitPlayer(collision);
                break;
            case "PlayerProjectile":
                DeSpawn();
                break;

            case "Walls":
                _speed *= -1;
                _state = EnemyState.Wait;
                break;

            case "WorldObject":
                _speed *= -1;
                _state = EnemyState.Wait;
                break;


        }
    }
}



