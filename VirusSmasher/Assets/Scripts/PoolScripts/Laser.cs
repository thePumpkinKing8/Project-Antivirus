using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : PoolObject
{
    [SerializeField] private float _speed;
    public float direction;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = new Vector2(_speed * direction, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<SpriteRenderer>().isVisible)
            OnDeSpawn();


    }
}
