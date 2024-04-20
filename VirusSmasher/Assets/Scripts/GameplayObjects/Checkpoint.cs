using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager _gameManager;
    private PlayerController _playerController;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _playerController = _gameManager.player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Save();
        //play any animation of saving
    }
    private void Save()
    {
        _playerController.ChangeHealth(100);
        _gameManager.Save(GetComponentInParent<Room>());
    }
}
