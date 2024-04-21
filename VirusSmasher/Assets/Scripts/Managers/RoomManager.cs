using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : Singleton<RoomManager>
{

    [HideInInspector] public Room startingRoom;
    private CameraController _camera;
    public Room currentRoom;

    public GameEvent loadEvent;

    public GameObject fadeEffect;


    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main.GetComponent<CameraController>();
    }

    public void LoadRoom(Room room, Door entrance = null)
    {
        fadeEffect.SetActive(true);
        InputManager.Instance.enabled = false;

        this.Wait(1.5f, () => { InputManager.Instance.enabled = true; });
        this.Wait(1.0f, () => { currentRoom.UnLoad(); });
        this.Wait(1.0f, () => { _camera.ChangeCamera(room._cameraType, room.cameraPos == null ? room.transform.position : room.cameraPos.position); });
        this.Wait(1.0f, () => { room.Load(entrance); });
        currentRoom = room;
        loadEvent.Raise();
        

    }

    public void LoadGame()
    {
        startingRoom._startingRoom = true;
    }
    
}
