using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public RoomColor roomColor;

    public void SwitchColor()
    {
        switch (roomColor)
        {
            case RoomColor.Green:
                GameManager.Instance.color = Color.green;
                break;
            case RoomColor.Blue:
                GameManager.Instance.color = Color.blue;
                break;
            case RoomColor.Orange:
                GameManager.Instance.color = Color.red;
                break;
        }

        GameManager.Instance.color = Color.green;
    }
}
public enum RoomColor
{ 
    Green,
    Blue,
    Orange
}

