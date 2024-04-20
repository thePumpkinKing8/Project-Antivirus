using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Door : MonoBehaviour
{
    public Door _connectedDoor;
    [HideInInspector] public Room parentRoom;
    private bool _isActive = true;
    [SerializeField] private Transform _exitLocation;
    [SerializeField] private float _triggerDistance;
    private Animator _anim;

    private bool _open = false;





    public bool Open
    {
        get
        {
            return _open;
        }
        set
        {
            if (_open != value)
            {
                _open = value;
                Animate(_open);
            }
        }
    }

    public GameObject pair; //other door to spawn when created

    [HideInInspector] public bool spawner = false;




    private void Awake()
    {
       
            parentRoom = GetComponentInParent<Room>();
            gameObject.name = $"Door{parentRoom.name}"; //to help keep track of what doors connect to what
            _anim = GetComponent<Animator>();

        

    }

    
    


    private void Update()
    {
        
        
            if (_triggerDistance <= (GameManager.Instance.player.transform.position - transform.position).magnitude)
                Open = false;

            else
                Open = true;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!_isActive)
            return;
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ExitRoom();
        }
    }

    public void EnterRoom()
    {
         GameManager.Instance.player.transform.position = _exitLocation.position; ;

    }

    private void ExitRoom()
    {
        RoomManager.Instance.LoadRoom(_connectedDoor.parentRoom, _connectedDoor);

        

    }

    private void Animate(bool value)
    {
        if(value)
        {
            _anim.SetBool("Open",true);
        }
        else
        {
            _anim.SetBool("Open", false);
        }
    }

}




