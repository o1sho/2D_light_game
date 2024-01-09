using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class SO_EntityIdleStateData : ScriptableObject
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;
}
