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

    public void LoadRoom(Room room, Door entrance = null)
    {
        currentRoom.UnLoad();
        _camera.ChangeCamera(room._cameraType, room.cameraPos == null ? room.transform.position : room.cameraPos.position);
        room.Load(entrance);
        currentRoom = room;
        
    }

    
}
