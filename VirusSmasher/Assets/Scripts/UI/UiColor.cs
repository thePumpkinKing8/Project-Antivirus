using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiColor : MonoBehaviour
{
    private Image[] images;
    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
    }
    public void ChangeColor()
    {
        foreach(Image image in images)
        {
            image.color = GameManager.Instance.color;
        }
    }
}
