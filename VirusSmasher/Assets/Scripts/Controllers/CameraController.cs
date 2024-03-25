using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CameraType _cameraType = CameraType.Static;
    public void ChangeCamera(CameraType type, Vector2 position)
    {
        if(type == CameraType.Static)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
        else if(type == CameraType.Dynamic)
        {
            transform.position = new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.player.transform.position.y, transform.position.z);
        }

        
    }
}
