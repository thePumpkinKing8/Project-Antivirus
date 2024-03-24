using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D c)
    {

        if (c.CompareTag("Player"))
        {

            //c.GetComponent<PlayerController>()._currentHealth -= 20;
            Destroy(gameObject);
        }

        if (c.gameObject.name == "Laser")
        {
            Destroy(gameObject);
        }

    }



}
