using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private Vector3 _spawnPosition;

    private bool _dead = false;

    public int health = 8;
    public int patrolSpeed = 8; 

    [Header("Attack Settings")]
    public int contactDamage = 20;
    public int dashDamage = 40;
    public int dashSpeed = 11;
    public int beamDamage = 30;

    [Header("Event")]
    [SerializeField] private GameEvent _virusKilled;

    private void Awake()
    {
        if (GameManager.Instance.CheckVirus(this))
            Destroy(gameObject);

        
        _spawnPosition = transform.position;
    }
    public void Load()
    {
        transform.position = _spawnPosition;
    }

    private void Die()
    {
        //play death animation, remove virus from game
        GameManager.Instance.AddDeadVirus(this);
        _virusKilled.Raise();

        Destroy(gameObject);

    }


    public void DashAttackState()
    {
        //arcing attack at the player with the peak of the arc being the players position
    }

    public void ProjectileAttackState()
    {
        //stays in place and fires projectiles at player
    }

    public void PatrolState()
    {
        //zigzags up and down around the screen
    }

    
}
