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
        _state = EnemyState.Patrol;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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



