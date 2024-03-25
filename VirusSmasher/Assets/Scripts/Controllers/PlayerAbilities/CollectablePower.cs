using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePower : MonoBehaviour
{
    [SerializeField] protected bool _collected = true;
    protected PlayerController _player;

    protected virtual void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    public void Collect()
    {
        _collected = true;
    }
}
