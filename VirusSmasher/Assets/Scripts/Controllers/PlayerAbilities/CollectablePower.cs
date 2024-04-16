using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePower : MonoBehaviour
{
    [SerializeField] protected bool _collected = true;

    public bool Collected
    {
        get { return _collected; }
        private set { _collected = value; }
    }

    protected PlayerController _player;

    protected virtual void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    public void Collect()
    {
        Collected = true;
    }

}
