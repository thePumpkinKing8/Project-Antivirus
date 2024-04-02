using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _damage = 20f;


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

    public virtual void Patrol()
    {

    }

    public virtual void Wait(float time)
    {

    }

    public virtual void Attack()
    {

    }


}

public enum EnemyState
{
    Patrol,
    Wait,
    Attack,
}