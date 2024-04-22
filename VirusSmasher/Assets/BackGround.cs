using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackGround : MonoBehaviour
{
    [SerializeField] private float _alpha;

    private Image _sprite;

    private void Awake()
    {
        _sprite = GetComponent<Image>();
 
    }
    // Update is called once per frame
    void Update()
    {
        if(_sprite.color.a != _alpha)
        {
            var temp = _sprite.color;
            temp.a = _alpha;
            _sprite.color = temp;
        }
    }
}
