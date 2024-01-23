using Oisho.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oisho.Weapons
{
    public class WeaponSpriteData : ComponentData
    {
        [field: SerializeField] public AttackSprites[] AttackData { get; private set; }
    }
}
