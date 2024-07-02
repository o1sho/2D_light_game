using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class SO_PlayerData : ScriptableObject
{
    [Header("Move State:")]
    public float movementVelocity = 10f;
    public float fastMovementVelocity = 15f;

    [Header("Jump State:")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("Roll State:")]
    public float rollVelocity = 30f;
    public float rollTime = 0.2f;
    //public float rollEndXMultiplier = 0.2f;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Characteristics")]
    public int vitality = 1;
    public int stamina = 1;
    public int strength = 1;
    public int accuracy = 1;

}
