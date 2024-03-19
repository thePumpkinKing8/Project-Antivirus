using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerController playerController;

    public Vector2 MoveInput { get; private set; }

    public bool IsIdle {  get; private set; }

    public bool IsMoving { get; private set; }

    public bool IsJumping { get; set; }

    public bool IsFalling { get; private set; } 

    public bool IsDashing { get; set; }

    private void Awake() => playerController = GetComponent<PlayerController>();
    
        
    

    // Update is called once per frame
    void Update()
    {
        MoveInput = InputManager.Move.ReadValue<Vector2>();

        IsIdle = !IsMoving && !IsFalling && !IsDashing;

        IsMoving = MoveInput != Vector2.zero || InputManager.Move.IsInProgress();

        IsFalling = playerController._rb.velocity.y < -0.2f && !playerController.IsGrounded() && !IsDashing;
    }
}
