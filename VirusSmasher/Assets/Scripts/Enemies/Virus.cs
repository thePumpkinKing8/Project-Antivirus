using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private Vector3 _spawnPosition;

    public int health = 8;
    public int patrolSpeed = 8; 

    [Header("attack settings")]
    public int contactDamage = 20;
    public int dashDamage = 40;
    public int dashSpeed = 11;
    public int beamDamage = 30;

    private void Awake()
    {
        _spawnPosition = transform.position;

    }
    public void Load()
    {
        transform.position = _spawnPosition;
    }


    
}
