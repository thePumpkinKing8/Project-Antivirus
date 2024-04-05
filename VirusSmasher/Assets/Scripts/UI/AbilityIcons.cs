using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIcons : MonoBehaviour
{
    public GameObject Dashpickup;
    public GameObject Firewallpickup;
    public GameObject Compresspickup;

    public GameObject Dashicon;
    public GameObject Firewallicon;
    public GameObject Compressicon;

    void Start()
    {
        Dashicon.SetActive(false);

        Firewallicon.SetActive(false);

        Compressicon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Dashpickup.activeSelf != false)
         {
         Dashicon.SetActive(true);
         }
         else
        {
        Dashicon.SetActive(false);
        }



        if (Firewallpickup.activeSelf != false)
        {
            Firewallicon.SetActive(true);
        }
        else
        {
            Firewallicon.SetActive(false);
        }



        if (Compresspickup.activeSelf != false)
        {
            Compressicon.SetActive(true);
        }
        else
        {
            Compressicon.SetActive(false);
        }
    }
}
