using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : PoolObject
{
    [HideInInspector] public Vector2 direction;
    [SerializeField] private float _speed;
    [HideInInspector] public int damage;
    // Start is called before the first frame update
    public void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2 direction = -collision.relativeVelocity.normalized;
            collision.gameObject.GetComponent<PlayerController>().OnHit(damage, direction);
            Debug.Log(direction);
        }
        else if(collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            OnDeSpawn();
    }
}
