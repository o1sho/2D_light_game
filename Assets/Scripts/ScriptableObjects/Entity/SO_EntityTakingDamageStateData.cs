using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTakingDamageStateData", menuName = "Data/State Data/Taking Damage State")]
public class SO_EntityTakingDamageStateData : ScriptableObject
{
    public float minTakingDamageTime = 1f;
    public float maxTakingDamageTime = 2f;
}
