using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Power _power;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            if (_power == Power.Dash)
                player.dashPower.Collect();
            else if (_power == Power.Shrink)
                player.GetComponent<Compression>().Collect();

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