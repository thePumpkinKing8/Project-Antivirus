using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _damage = 20f;


    //spawns and despawns object on death and when the room loads/unloads
    //this prevents us from having to instantiate new enemies everytime a room loads
    public virtual void DeSpawn()
    {
        gameObject.SetActive(false);
    }

    public virtual void Spawn()
    {
       gameObject.SetActive(true);
    }
}
