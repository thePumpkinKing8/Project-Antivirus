using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{

    [SerializeField] private RawImage background;

    [SerializeField] private float x, y;

    void Update()
    {
     background.uvRect = new Rect(background.uvRect.position + new Vector2(x, y) * Time.deltaTime,background.uvRect.size);   
    }
}
