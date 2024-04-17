using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private Vector3 _spawnPosition;

    private bool _dead = false;

    public int maxHealth = 8;
    private int _health;

    public int cutSceneTime = 1;

    [Header("PatrolSettings")]
    public int patrolSpeed = 8;
    public int maxDistance = 8;
    public float maxPatrolTime = 5f;

    [Header("Attack Settings")]
    public int contactDamage = 20;
    [Header("Dash Attack Settings")]
    public int dashDamage = 40;
    public int dashSpeed = 11;
    private bool _dIsRunning;

    [Header("Projectile Attack Settings")]
    public int projectileDamage = 30;
    public int projectileChargeTime = 2;
    [Tooltip("number of shots virus will fire at player")]
    public int shotAmount = 3;
    public float shotDelay = 0.5f;
    [SerializeField] private Transform _projectileSpawner;
    private bool _pIsRunning;

    [Header("Event")]
    [SerializeField] private GameEvent _virusKilled;


    private Vector3 _direction;

    private Rigidbody2D _rb;

    private PlayerController _player;

    private BossStates _bossStates;

    private float _patrolTimer;

    public Vector3 Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            var distance =  (value - transform.position).normalized;

            _direction = new Vector3(distance.x > 0 ? 1 : -1, distance.y > 0 ? 1 : -1, 0);
        }
    }


    private void Awake()
    {
        if (GameManager.Instance.CheckVirus(this))
            Destroy(gameObject);
        _health =  maxHealth;
        _rb = GetComponent<Rigidbody2D>();

        _direction = new Vector2(-1f, -1f);
        _spawnPosition = transform.position;

        _bossStates = BossStates.Idle;
    }

    private void Start()
    {
        _player = GameManager.Instance.player;

    }

    private void Update()
    {
        _rb.velocity = Vector2.zero;
        switch (_bossStates)
        { 
            case BossStates.Patrol:
                PatrolState();
                break;
            case BossStates.Dash:
                DashAttackState();
                break;
            case BossStates.Shoot:
                ProjectileAttackState();
                break;
            case BossStates.Idle:
                IdleState();
                break;
        }
         
        
    }


    private void ChangeState(BossStates state)
    {
        _bossStates = state;
    }



    public void Load()
    {
        transform.position = _spawnPosition;
        _health = maxHealth;
        _bossStates = BossStates.Idle;
        _patrolTimer = 0;
    }

    private void Die()
    {
        //play death animation, remove virus from game
        //GameManager.Instance.AddDeadVirus(this);
        _virusKilled.Raise();

        Destroy(gameObject);

    }


    public void DashAttackState()
    {
        //arcing attack at the player with the peak of the arc being the players position
        if(!_dIsRunning)
        {
            StartCoroutine(DashAttack());
        }
    }

    public void ProjectileAttackState()
    {
        //stays in place and fires projectiles at player
        if (!_pIsRunning)
        {
            StartCoroutine(ProjectileAttack());
        }
        
    }

    private void ShootProjectile()
    {
        EnemyProjectile projectile = PoolManager.Instance.Spawn("EnemyProjectile").GetComponent<EnemyProjectile>();
        projectile.transform.position = _projectileSpawner.position;
        projectile.direction = ((_player.transform.position - transform.position).normalized);
        projectile.Shoot();
    }

    public void PatrolState()
    {
        //zigzags up and down around the screen

   

        transform.Translate(Direction * patrolSpeed * Time.deltaTime);

        _patrolTimer += Time.deltaTime;

        if(_patrolTimer >= maxPatrolTime)
        {
            _patrolTimer = 0;
            BossStates newState = (BossStates)Random.Range(1, 3);
            ChangeState(newState);
        }
    }

    private void SwitchDirection(Vector2 target)
    {
        Direction = target;
    }

    public void IdleState()
    {
        StartCoroutine(Idle());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            _direction = Vector3.Reflect(_direction, collision.contacts[0].normal) ;
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _player.OnHit(contactDamage, -collision.relativeVelocity.normalized);
            
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            TakeDamage();
        }
    }


    private void TakeDamage()
    {
        _health -= 1;
        Debug.Log("Health");
        //play any damage effects

        if (_health <= 0)
            Die();
    }

    IEnumerator Idle()
    {
        yield return new WaitForSeconds(cutSceneTime);
        _bossStates = BossStates.Patrol;
        yield return null;
    }

    IEnumerator ProjectileAttack()
    {
        _pIsRunning = true;
        yield return new WaitForSeconds(projectileChargeTime);

        for(int i = 0; i < shotAmount; i++)
        {
            ShootProjectile();
            yield return new WaitForSeconds(shotDelay);
        }
        ChangeState(BossStates.Patrol);
        _pIsRunning = false;
        yield return null;
    }


    IEnumerator DashAttack()
    {
        _dIsRunning = true;
        float count = 0f;
        var target = new Vector3( _player.transform.position.x, _player.transform.position.y -5);
        var initialPosition = transform.position;
        var direction = Mathf.Sign((target - initialPosition).x);

        Vector2 endPoint = new Vector2( target.x + (target - initialPosition).x, initialPosition.y);
        
        
        RaycastHit2D ray = Physics2D.Raycast(initialPosition, (endPoint - (Vector2)initialPosition).normalized, (endPoint - (Vector2)initialPosition).magnitude, ~LayerMask.GetMask("Enemy"));
        if(ray.collider != null)
        {
            endPoint = new Vector2(ray.point.x - (direction * transform.localScale.x), initialPosition.y);
            
        }
        Debug.Log(endPoint);
        while (count < 1.0f)
        {
            count += Time.fixedDeltaTime;
            Vector3 m1 = Vector3.Lerp(initialPosition, target, count);
            Vector3 m2 = Vector3.Lerp(target, endPoint, count);
            transform.position = Vector3.Lerp(m1,m2,count);
            yield return new WaitForFixedUpdate();
        }
        ChangeState(BossStates.Patrol);
        _rb.velocity = Vector2.zero;
        _dIsRunning = false;
        yield return null;
    }

}

public enum BossStates
{
    Patrol,
    Dash,
    Shoot,
    Death,
    Idle
}

