using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerController playerController;

    private Animator _animator;

    public Vector2 MoveInput { get; private set; }

    public bool IsIdle {  get; private set; }

    public bool IsMoving { get; private set; }

    public bool IsJumping { get; set; }

    public bool IsFalling { get; private set; } 

    public bool IsDashing { get; set; }

    public bool IsShielding {  get; set; }

    private bool _isSmall;
   public bool IsSmall 
    {
        get
        {
            return _isSmall;
        } 
        set
        {
            _animator.SetBool("Compressed", value);
            _isSmall = value;
        }
    }
    

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        _animator = playerController.GetComponent<Animator>();
    }
    
    
        
    

    // Update is called once per frame
    void Update()
    {
        MoveInput = InputManager.Move.ReadValue<Vector2>();

        IsIdle = !IsMoving && !IsFalling && !IsDashing;

        IsMoving = MoveInput != Vector2.zero || InputManager.Move.IsInProgress();

        IsFalling = playerController._rb.velocity.y < -0.2f && !playerController.IsGrounded() && !IsDashing;

        IsShielding = !IsDashing && !IsSmall && InputManager.Shield.IsPressed();
    }

   
}
