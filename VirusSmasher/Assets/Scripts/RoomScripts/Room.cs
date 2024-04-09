using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [Tooltip("static camera stay still, a dynamic camera will follow the player")]
    public CameraType _cameraType;
    [SerializeField] private bool _startingRoom;
    public Transform cameraPos;

    private GameEvent _loadEvent;

    private void Awake()
    {
        if (!cameraPos)
            cameraPos = transform;

        foreach (GameEvent gEvent in Resources.LoadAll<GameEvent>("Events"))
        { 
            if(gEvent.name == "RoomLoadEvent")
                _loadEvent = gEvent;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        if (_startingRoom)
        {
            RoomManager.Instance.currentRoom = this;
            RoomManager.Instance.LoadRoom(this);
            GameManager.Instance.SetArea();
            Load();
        }
        else
            UnLoad();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load(Door entrance = null )
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        foreach(SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = GameManager.Instance.color;
        }
        foreach(Tilemap tilemap in GetComponentsInChildren<Tilemap>())
        {
            tilemap.color = GameManager.Instance.color;
        }
        if (entrance != null)
        {
            entrance.EnterRoom();
        }
        else
            GameManager.Instance.player.transform.position = cameraPos.position;

        _loadEvent.Raise();
        
    }

    public void UnLoad()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}

public enum CameraType
{
    Static,
    Dynamic
}
