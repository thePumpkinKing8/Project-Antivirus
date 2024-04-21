using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="SaveData", menuName = "SaveData")]
public class SaveData : ScriptableObject
{
    //spawn position/last save point

   // private List<Virus> _deadVirusList;

   // private bool _dashCollected;
   // private bool _compressCollected;
   // private bool _shieldCollected;

   // private Room _saveRoom;

    public List<Virus> DeadVirusList{ get; private set; }

    public bool DashCollected { get; private set; }
    public bool CompressCollected { get; private set; }
    public bool ShieldCollected { get; private set; }

    public Room SaveRoom { get; private set; }


    public void Save(Room room, List<Virus> list, bool dash, bool compress, bool shield)
    {
        SaveRoom = room;

        DeadVirusList = list;

        DashCollected = dash;

        CompressCollected = compress;

        ShieldCollected = shield;
    }

    public void Load()
    {
        RoomManager.Instance.startingRoom = SaveRoom;
    }
}
