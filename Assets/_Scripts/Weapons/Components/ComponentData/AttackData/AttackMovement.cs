using System;
using System.Collections;
using UnityEngine;

namespace Oisho.Weapons
{
    [Serializable]
    public class AttackMovement : AttackData
    {
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public float Velocity { get; private set; } 
    }
}