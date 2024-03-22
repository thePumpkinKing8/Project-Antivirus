using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compression : CollectablePower
{
    private Vector3 _playerScale;

    private bool _compressed = false;


    //multiple press variables
    private float press = 0;
    private float time = 0;
    [Tooltip("the time the player has to input a second down input in order to shrink the player")]
    [SerializeField] private float inputDelay = .5f;
    protected override void Awake()
    {
        base.Awake();
        _playerScale = _player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (InputManager.Shrink.triggered)
        {
            Shrink();
            return;
        }
    }

    private void Shrink()
    {
        //divides the players local scale by the "shrinkValue"
        if (!_compressed)
        {
            _player.transform.localScale = _player.transform.localScale / _player.settings.shrinkValue;
            _compressed = true;
        }
        else
        {
            _player.transform.localScale = _playerScale;
            _compressed = false;    
        }
    }

}
