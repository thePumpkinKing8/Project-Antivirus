using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterWall : MonoBehaviour
{
    private Transform _projectileSpawner;
    public float shotDelay = .05f;
    private bool available = true;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _projectileSpawner = GetComponentInChildren<Transform>();
        _boxCollider = GetComponentInChildren<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (available)
            Fire();

        if (GameManager.Instance.player.shieldPower._shield.activeSelf)
            _boxCollider.enabled = false;
        else
            _boxCollider.enabled = true;
    }

    private void Fire()
    {
        available = false;
        WallProjectile projectile = PoolManager.Instance.Spawn("WallProjectile").GetComponent<WallProjectile>();
        projectile.transform.position = _projectileSpawner.position;
        projectile.direction = -this.transform.up;
        projectile.Shoot();
        this.Wait(shotDelay, () => { available = true; });
    }
}
