using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CameraType _cameraType = CameraType.Static;
    private Camera _camera;
    [SerializeField] private float _size = 8f;

    private void Awake()
    {
        _camera = Camera.main;
        _camera.orthographicSize = _size;
    }
    public void ChangeCamera(CameraType type, Vector2 position)
    {
        if(type == CameraType.Static)
        {
            transform.parent = null;
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
        else if(type == CameraType.Dynamic)
        {
            transform.position = new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.player.transform.position.y, transform.position.z);
            transform.parent = GameManager.Instance.player.transform;
        }
    }

    //note for later: in dynamic mode camera should not follow player vertically until they land on a platform
}
