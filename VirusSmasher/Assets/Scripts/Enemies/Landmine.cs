using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : Enemy
{
    private void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2 direction = -c.relativeVelocity.normalized;
            c.gameObject.GetComponent<PlayerController>().OnHit(_damage, direction);
            Debug.Log(direction);
            DeSpawn();
        }

        if (c.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            DeSpawn();
        }

    }



}
