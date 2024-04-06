using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    private PlayerController _player;

    private Transform _projectileSpawner;

    [SerializeField] private int _agroDistance;

    private void Awake()
    {
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

        _state = EnemyState.Wait;
    }

    
}
