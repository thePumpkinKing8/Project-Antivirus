using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    private Image healthbar;


    private float _maxHealth = 100f;



    private void Start()
    {
        healthbar = GetComponent<Image>();
        GameManager.Instance.player.healthbar = this;
        _maxHealth = GameManager.Instance.player.settings.maxHealth;
    }

    public void ChangeHealthFill(float value)
    {
        if(value > _maxHealth) 
            value = _maxHealth;
        healthbar.fillAmount = value / _maxHealth;
    }
}
