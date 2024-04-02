using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Tooltip("static camera stay still, a dynamic camera will follow the player")]
    public CameraType _cameraType;
    [SerializeField] private bool _startingRoom;
    public Transform cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        if (_startingRoom)
        {
            RoomManager.Instance.currentRoom = this;
            RoomManager.Instance.LoadRoom(this, GetComponentInChildren<Door>());

        }
        else
            UnLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load(Door entrance)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        entrance.EnterRoom();
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
