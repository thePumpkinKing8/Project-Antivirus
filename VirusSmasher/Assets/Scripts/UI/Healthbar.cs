using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public Image healthbar;


    public float health = 100f;




    void Start()
    {
        
    }

    
    void Update()
    {
      if (health == 0)
        {

        }  
    }


    public void takeHealth(float damage)
    {
        health -= damage;

        healthbar.fillAmount = health / 100;
    }


    public void addHealth(float heal)
    {
        health += heal;

        health = Mathf.Clamp(health, 0, 100);

        healthbar.fillAmount = health / 100;
    }



}
