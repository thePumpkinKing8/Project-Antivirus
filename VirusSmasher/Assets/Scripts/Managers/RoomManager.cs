using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    private CameraController _camera;
    public Room currentRoom; 

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main.GetComponent<CameraController>();
    }

    public void LoadRoom(Room room, Door entrance)
    {
        currentRoom.UnLoad();
        room.Load(entrance);
        currentRoom = room;
        _camera.ChangeCamera(room._cameraType, room.transform.position);
    }

    
}
