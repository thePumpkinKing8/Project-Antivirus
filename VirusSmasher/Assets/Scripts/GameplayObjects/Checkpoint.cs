using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager _gameManager;
    private PlayerController _playerController;

    public AudioClip checkPointSFX;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playerController = _gameManager.player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Save();
            Debug.Log("save");
        }
    }
    private void Save()
    {
        GetComponent<Animator>().SetBool("gotten", true);
        _playerController.ChangeHealth(100);
        _gameManager.Save(GetComponentInParent<Room>());
    }
}
