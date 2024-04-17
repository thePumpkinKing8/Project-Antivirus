using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="SaveData", menuName = "SaveData")]
public class SaveData : MonoBehaviour
{
    //spawn position/last save point

    private List<Virus> _deadVirusList;

    private bool _dashCollected;
    private bool _compressCollected;
    private bool _shieldCollected;

    public List<Virus> DeadVirusList{ get; private set; }

    public bool DashCollected { get; private set; }
    public bool CompressCollected { get; private set; }
    public bool ShieldCollected { get; private set; }


    public void Save(List<Virus> list, bool dash, bool compress, bool shield)
    {
        DeadVirusList = list;

        DashCollected = dash;

        CompressCollected = compress;

        ShieldCollected = shield;
    }

    public void Load()
    {
        
    }
}
