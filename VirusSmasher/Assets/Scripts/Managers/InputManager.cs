using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputActions _actions;

    //player actions
    public static InputAction Move => Instance._actions.Player.Movement;
    public static InputAction Jump => Instance._actions.Player.Jump;
    public static InputAction Shoot => Instance._actions.Player.Shoot;
    public static InputAction Dash => Instance._actions.Player.Dash;
    public static InputAction Shrink => Instance._actions.Player.Compress;
    public static InputAction Shield => Instance._actions.Player.Shield;


    protected override void OnAwake()
    {
        Initialize();
        _actions = new InputActions();
    }
   

    private void OnEnable() => _actions.Enable();
    private void OnDisable() => _actions.Disable();


    /// <summary>
    /// Special singleton initializer method.
    /// </summary>
    public new static void Initialize()
    {
        var prefab = Resources.Load<GameObject>("Managers/InputManager");
        if (prefab == null) throw new Exception("Missing InputManager prefab!");

        var instance = Instantiate(prefab);
        if (instance == null) throw new Exception("Failed to instantiate InputManager prefab!");

        instance.name = "Managers.InputManager (Singleton)";
    }
}
