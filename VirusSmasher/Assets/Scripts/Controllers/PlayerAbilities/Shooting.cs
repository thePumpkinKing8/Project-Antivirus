using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private PlayerController _player;
    [SerializeField] private Transform _spawner;
    private float _timer = 0;
    private bool _canShoot = true;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_canShoot)
        {
            if (_timer >= _player.settings.shootingCooldown)
                _canShoot = true;
            else
                _timer += Time.deltaTime;
        }
        if (InputManager.Shoot.triggered)
        {
            if(_canShoot) 
                Shoot();
        }
            
    }

    private void Shoot()
    {
        if (GameManager.Instance.Pause)
            return;
        _timer = 0;
        _canShoot = false;
        var projectile = PoolManager.Instance.Spawn("Laser").GetComponent<Laser>();
        projectile.direction = _player.lastDirection;
        projectile.transform.position = _spawner.position;
        projectile.OnSpawn();
    }
}
