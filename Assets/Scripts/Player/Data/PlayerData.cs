using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
// Store stat of player (e.g. speed, attack,...)
public class PlayerData : EntityData
{
    [Header("Move State")]
    public float movementSpeed = 10f;
    [Header("Focus State")]
    public float walkSpeed = 3f;
    [Header("Jump State")]
    public float jumpForce = 15f;
    public int jumpCount = 2;

    [Header("In Air State")]
    public float coyoteTime = 0.1f;

    [Header("Ground Check Variables")]
    public float GroundRadius = 0.5f;
    public LayerMask GroundLayer;
    public float WallCheckDistance = 0.3f;

    [Header("Wall Slide State")]
    public float WallSlideVelocity = 0.5f; 

    [Header("Wall Jump State")]
    public float WallJumpVelocity = 15f;
    public float WallJumpTime = 0.4f;
    public Vector2 WallJumpAngle = new Vector2(1,2);

    [Header("Ledge Jump State")]
    public Vector2 StartOffset;
    public Vector2 StopOffset;
    public float QuickClimbForceMultiplier = 0.3f;
}
