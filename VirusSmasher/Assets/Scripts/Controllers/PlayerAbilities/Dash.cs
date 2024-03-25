using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : CollectablePower
{
    [HideInInspector] public bool grounded;

    [HideInInspector] public float timer;
    protected override void Awake()
    {
        base.Awake();

        timer = _player.settings.dashCoolDown;
    }


    // Update is called once per frame
    void Update()
    {
        if ( timer < _player.settings.dashCoolDown)
            timer += Time.deltaTime;

    }

    public bool SetDash()
    {
        if(!_collected)
            return false;
        if(_player.grounded && timer >= _player.settings.dashCoolDown)
            return true;
        else
            return false;
    }
  
}
