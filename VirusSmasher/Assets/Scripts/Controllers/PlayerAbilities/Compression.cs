using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compression : CollectablePower
{
    private Vector3 _playerScale;

    private bool _compressed = false;
    protected override void Awake()
    {
        base.Awake();
        _playerScale = _player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Shrink.triggered)
        {
            Shrink();
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
