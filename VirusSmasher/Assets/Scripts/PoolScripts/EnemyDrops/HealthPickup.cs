using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : DroppedItem
{
    [SerializeField] private float healValue;

    protected override void GetItem()
    {
        base.GetItem();
        GameManager.Instance.player.ChangeHealth(healValue);
        OnDeSpawn();
    }
}
