using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _damage = 20f;

    protected EnemyState _state;
    [SerializeField] protected float _waitTime;
    protected float _timer;
    protected Vector3 _spawnPosition;
    private bool _active = false;
    

    protected virtual void Awake()
    {
        _spawnPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (!GameManager.Instance.Pause)
        {
            if (_active)
            {
                switch (_state)
                {
                    case EnemyState.Patrol:
                        Patrol();
                        break;
                    case EnemyState.Wait:
                        WaitState(_waitTime);
                        break;
                    case EnemyState.Attack:
                        Attack();
                        break;
                }
            }
        }
            
        
    }

    //spawns and despawns object on death 
    //this prevents us from having to instantiate new enemies everytime a room loads
    public virtual void DeSpawn()
    {
        var drop = PoolManager.Instance.Spawn("HealthPickup");
        drop.transform.position = transform.position;
        gameObject.SetActive(false);
    }

    public virtual void Spawn()
    {
       gameObject.SetActive(true);
    }

    /// <summary>
    /// applies damage and force to player
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void HitPlayer(Collision2D collision)
    {
        Vector2 direction = -collision.relativeVelocity.normalized;
        collision.gameObject.GetComponent<PlayerController>().OnHit(_damage, direction);
        Debug.Log(direction);
    }

    public virtual void Patrol()
    {

    }

    public virtual void WaitState(float time)
    {
        _timer += Time.deltaTime;
        if (_timer >= _waitTime)
        {
            _state = EnemyState.Patrol;
            _timer = 0;
        }
    }

    public virtual void Attack()
    {

    }

    public virtual void Load()
    {
        transform.position = _spawnPosition;
        _state = EnemyState.Wait;
        this.Wait(1f, () => { _active = true; });
    }


    public virtual void SetAlive()
    {
        _active = false;
    }


}

public enum EnemyState
{
    Patrol,
    Wait,
    Attack,
}