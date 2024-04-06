using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyProjectile : PoolObject
{
    [HideInInspector] public Vector2 direction;
    [SerializeField] private float _speed;
     public int damage;

    [SerializeField] private GameEvent _shieldTrigger;
    // Start is called before the first frame update
    public void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
      
            Debug.Log("hit");
            _shieldTrigger.Raise(damage);
            OnDeSpawn();
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
            Vector2 direction = -collision.relativeVelocity.normalized;
            collision.gameObject.GetComponent<PlayerController>().OnHit(damage, direction);
            Debug.Log(direction);
            OnDeSpawn();
        }
  
        else if(collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            OnDeSpawn();
    }
}
