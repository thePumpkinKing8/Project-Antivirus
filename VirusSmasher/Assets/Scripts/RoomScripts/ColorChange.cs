using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public RoomColor roomColor;
    public AudioClip areaBGM;
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
        GameManager.Instance.player.GetComponent<SpriteRenderer>().color = GameManager.Instance.color;

        if(RoomManager.Instance.areaBGM != areaBGM)
        {
            RoomManager.Instance.areaBGM = areaBGM;
            AudioManager.Instance.ChangeMusic(areaBGM);
        }
    }
}
public enum RoomColor
{ 
    Green,
    Blue,
    Orange
}

