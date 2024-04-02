using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : PoolObject
{
    [SerializeField] private float _speed;
    [HideInInspector] public float direction;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = GameManager.Instance.color;
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<SpriteRenderer>().isVisible)
            OnDeSpawn();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        _rb.velocity = new Vector2(_speed * direction, 0);
        if (direction < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnDeSpawn();
    }
}
