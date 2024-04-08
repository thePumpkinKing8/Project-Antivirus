using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compression : CollectablePower
{
    private Vector3 _playerScale;

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
    private void Start()
    {
        _player.inputController.IsSmall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_player.inputController.IsDashing)
            HandleInput();
    }

    private void HandleInput()
    {
        if (InputManager.Shrink.triggered)
        {
            if (!_collected)
                return;
            Shrink();
            return;
        }
    }

    private void Shrink()
    {
        //divides the players local scale by the "shrinkValue"
        if (!_player.inputController.IsSmall)
        {
            _player.transform.localScale = new Vector3((_player.transform.localScale.x / _player.settings.shrinkValue) , (_player.transform.localScale.y / _player.settings.shrinkValue) , (_player.transform.localScale.z / _player.settings.shrinkValue));
            
            _player.inputController.IsSmall = true;
        }
        else
        {
            _player.transform.localScale = new Vector3( _playerScale.x * _player.lastDirection, _playerScale.y, _playerScale.z) ;
            _player.inputController.IsSmall = false;    
        }
    }

}
