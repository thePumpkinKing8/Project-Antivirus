using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Represents player settings for use in the game.
/// </summary>
[CreateAssetMenu(fileName = "Player", menuName = "Player/Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("PlayerSettings")]
    public float movementSpeed = 5f;
    public float jumpHeight = 10f;
    [Tooltip("lower the value lower the jump")]
    public float lowJumpMultiplier = 5f;
    public float maxHealth = 100f;
    public float invulnFrameTime = 1.5f;
    public float knockBackForce = 6f;
    [Tooltip("time player has no control after being hit")]
    public float hitTime = .25f;

    [Header("DashSettings")]
    [Tooltip("distance dash travels")]
    public float dashDistance = 5f;
    [Tooltip("time for dash to complete, the lower the faster")]
    public float dashTime = 0.5f;
    public float dashCoolDown = 0.6f;

    [Header("ShrinkSettings")]
    [Tooltip("amount the players scale will be divided by")]
    public float shrinkValue = 2f;

    [Header("GroundCheckSettings")]
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayerMask;

}
