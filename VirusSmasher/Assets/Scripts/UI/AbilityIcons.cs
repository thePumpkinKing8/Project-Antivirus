using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIcons : MonoBehaviour
{
    private PlayerController player;
    public GameObject Dashicon;
    public GameObject Firewallicon;
    public GameObject Compressicon;

    void Start()
    {
        player = GameManager.Instance.player;
        Dashicon.SetActive(false);

        Firewallicon.SetActive(false);

        Compressicon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.dashPower.Collected)
         {
         Dashicon.SetActive(true);
         }
         else
        {
        Dashicon.SetActive(false);
        }



        if (player.shieldPower.Collected)
        {
            Firewallicon.SetActive(true);
        }
        else
        {
            Firewallicon.SetActive(false);
        }



        if (player.compressionPower.Collected)
        {
            Compressicon.SetActive(true);
        }
        else
        {
            Compressicon.SetActive(false);
        }
    }
}
