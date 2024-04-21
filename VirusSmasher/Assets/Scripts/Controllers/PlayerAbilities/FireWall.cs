using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : CollectablePower
{
    [SerializeField] public GameObject _shield;

    private int _maxShieldHealth;
    private int _shieldHealth;
    private float _rechargeTime;
    private float _timer;
    private bool _destroyed;

    protected override void Awake()
    {
        base.Awake();
        _maxShieldHealth = _player.settings.shieldHealth;
        _rechargeTime = _player.settings.rechargeTime;
        _shieldHealth = _maxShieldHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        _shield.GetComponent<SpriteRenderer>().color = GameManager.Instance.color;
        _shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_collected)
        {
            if(!_destroyed)
            {
                if (_player.inputController.IsShielding)
                {
                    _shield.GetComponent<SpriteRenderer>().color = GameManager.Instance.color;
                    _shield.SetActive(true);
                }
                else
                    _shield.SetActive(false);
            }
            else
            {
                _timer += Time.deltaTime;
                if (_timer >= _rechargeTime)
                {
                    _destroyed = false;
                }
            }
            
        }
        
    }

    //this is called by an event
    public void HitShield(object value)
    {
        var damage = value as int?;
        if(!(value is int))
        {
            Debug.Log("wrong data type given in function call");
            return; 
        }

        _shieldHealth -= damage.Value;

        if( _shieldHealth <= 0 )
        {
            _destroyed = true;
            _timer = 0;
            _shield.SetActive(false);
        }
    }
}
