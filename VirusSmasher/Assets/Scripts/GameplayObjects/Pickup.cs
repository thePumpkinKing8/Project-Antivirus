using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioClip pickupSFX;
    [SerializeField] private Power _power;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AudioManager.Instance.otherPlay(pickupSFX);
            var player = collision.GetComponent<PlayerController>();
            if (_power == Power.Dash)
                player.dashPower.Collect();
            else if (_power == Power.Shrink)
                player.GetComponent<Compression>().Collect();
            else if (_power == Power.Shield)
                player.GetComponent<FireWall>().Collect();

            Destroy(gameObject);
        }
    }

}
public enum Power
{
    Dash,
    Shrink,
    Shield
}