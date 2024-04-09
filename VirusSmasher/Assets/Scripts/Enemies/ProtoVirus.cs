using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoVirus : Enemy
{

    protected VirusCounter Viruscounter;

    // Start is called before the first frame update
    void Start()
    {
        Viruscounter = GameObject.Find("UI Manager").GetComponent<VirusCounter>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        _state = EnemyState.Patrol;
    }


    private void OnCollisionEnter2D(Collision2D c)
    {


        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2 direction = -c.relativeVelocity.normalized;
            c.gameObject.GetComponent<PlayerController>().OnHit(_damage, direction);
            Debug.Log(direction);
            DeSpawn();

            Viruscounter.UpdateScore(1);
        }

        if (c.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            DeSpawn();

            Viruscounter.UpdateScore(1);
        }

    }



}
