using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private PlayerController _player;
    [SerializeField] private Transform _spawner;



    private void Awake()
    {
        _player = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void Shoot()
    {
        var projectile = PoolManager.Instance.Spawn("Laser").GetComponent<Laser>();
        projectile.direction = _player.lastDirection;
        projectile.transform.position = _spawner.position;
        projectile.OnSpawn();
    }
}
