using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : PoolObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            GetItem();
            
    }

    protected virtual void GetItem()
    {

    }
}
