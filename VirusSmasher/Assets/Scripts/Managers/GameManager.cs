using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    public PlayerController player;

    public Color32 color = Color.green;

    private List<Virus> viruses;

    [SerializeField] private SaveData _saveData;


    public void SetArea()
    {
        if (SceneManager.GetActiveScene().name == "PrototypeLevel")
        {
            color = Color.green;
        }
        Debug.Log(color);
        player.GetComponent<SpriteRenderer>().color = color;
    }

    //adds virus to list of dead
    public void AddDeadVirus(Virus virus)
    {
        viruses.Add(virus);
    }

    //checks if virus is alive
    public bool CheckVirus(Virus virus)
    {
        if(viruses != null)
        {
            foreach (Virus dead in viruses)
            {
                if (virus == dead)
                {
                    return true;
                }
            }
        }
       
        return false;
    }

    public void Save()
    {
        _saveData.Save(viruses, player.dashPower.Collected, player.compressionPower.Collected, player.shieldPower.Collected);
    }

    public void Load()
    {
        //set required values to those in saveData
    }

}
