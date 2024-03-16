using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float _dashForce = 5f;

    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Dash.triggered)
        {
            if(_player.IsGrounded())
                _player._canDash = true;
            Blink();
        }
    }
    /// <summary>
    /// makes the player dash
    /// </summary>
    private void Blink()
    {
        if (!_player._canDash)
            return;
        Debug.Log("hi");
        _player._rb.velocity = InputManager.Move.ReadValue<Vector2>().normalized * _dashForce;
        _player._canDash = false;
    }
}
