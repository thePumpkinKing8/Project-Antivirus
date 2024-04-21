using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProjectile : PoolObject
{
    [SerializeField] private int _speed;
    [HideInInspector] public Vector2 direction;

    public void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnDeSpawn();
    }
}
