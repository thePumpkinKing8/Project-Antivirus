using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : Singleton<RoomManager>
{

    [HideInInspector] public Room startingRoom;
    [HideInInspector] public AudioClip areaBGM;
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
        GameManager.Instance.player.GetComponent<SpriteRenderer>().enabled = false;
        InputManager.Instance.enabled = false;
        this.Wait(1.5f, () => { InputManager.Instance.enabled = true; });
        this.Wait(1.0f, () => { GameManager.Instance.player.GetComponent<SpriteRenderer>().enabled = true; });
        this.Wait(1.0f, () => { _camera.ChangeCamera(room._cameraType, room.cameraPos == null ? room.transform.position : room.cameraPos.position); });
        this.Wait(1.0f, () => { room.Load(entrance); });
        if(AudioManager.Instance.musicSource.clip != areaBGM)
            AudioManager.Instance.ChangeMusic(areaBGM);
        currentRoom.UnLoad();
        currentRoom = room;
        

    }

    public void LoadGame()
    {
        startingRoom._startingRoom = true;
        LoadGame();
    }
    
}
