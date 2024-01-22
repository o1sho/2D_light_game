using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class SO_EntityData : ScriptableObject
{
    [Header("Idle State:")]
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;

    [Header("Move State:")]
    public float movementSpeed = 3f;

    [Header("Taking Damage State:")]
    public float minTakingDamageTime = 1f;
    public float maxTakingDamageTime = 2f;

    [Header("Charge State:")]
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;

    [Header("Look For Player State:")]
    public int amountOfTurns = 2;
    public float timeBetweenTurns = 0.75f;

    [Header("Melee Attack State:")]
    public float strength;
    public Vector2 angle;
}
