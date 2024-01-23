using System.Collections;
using UnityEngine;

namespace Oisho.Weapons
{
    public class MovementData : ComponentData
    {
        [field: SerializeField] public AttackMovement[] AttackData { get; private set; }
    }
}