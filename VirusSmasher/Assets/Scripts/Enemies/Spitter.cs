using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    private PlayerController _player;

    private Transform _projectileSpawner;

    [SerializeField] private int _agroDistance;

    public AudioClip fireSFX;

    protected override void Awake()
    {
        base.Awake();
        _projectileSpawner = GetComponentInChildren<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _state = EnemyState.Patrol;
        _player = GameManager.Instance.player;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(_state == EnemyState.Patrol)
        {
            //check player distance and fire if they are close
            if(Mathf.Abs((_player.transform.position - transform.position).magnitude) < _agroDistance)
            {
                _state = EnemyState.Attack;
            }
        }
        base.Update();
    }

    public override void Attack()
    {
        EnemyProjectile projectile = PoolManager.Instance.Spawn("EnemyProjectile").GetComponent<EnemyProjectile>();
        projectile.transform.position = _projectileSpawner.position;
        projectile.direction = ((_player.transform.position - transform.position).normalized);
        projectile.Shoot();
        AudioManager.Instance.EnemyPlay(fireSFX);

        _state = EnemyState.Wait;
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2 direction = -c.relativeVelocity.normalized;
            c.gameObject.GetComponent<PlayerController>().OnHit(_damage, direction);
            Debug.Log(direction);
        }

        if (c.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            DeSpawn();
        }

    }



}
