using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Door _connectedDoor;
    [HideInInspector] public Room parentRoom;
    private bool _isActive = true;
    [SerializeField] private Transform _exitLocation;
    [SerializeField] private float _triggerDistance;

    private void Awake()
    {
        parentRoom = GetComponentInParent<Room>();
    }

    private void Update()
    {
        if(_triggerDistance <= (GameManager.Instance.player.transform.position - transform.position).magnitude) 
        {
            //play open animation
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!_isActive)
            return;
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ExitRoom();
        }
    }

    public void EnterRoom()
    {
        GameManager.Instance.player.transform.position = _exitLocation.position;
    }

    private void ExitRoom()
    {
        RoomManager.Instance.LoadRoom(_connectedDoor.parentRoom, _connectedDoor);
    }
}
