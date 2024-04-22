using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : DroppedItem
{
    [SerializeField] private float healValue;
    public AudioClip pickSFX;
    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = GameManager.Instance.color;
    }
    protected override void GetItem()
    {
        base.GetItem();
        GameManager.Instance.player.ChangeHealth(healValue);
        AudioManager.Instance.otherPlay(pickSFX);
        OnDeSpawn();
    }
}
