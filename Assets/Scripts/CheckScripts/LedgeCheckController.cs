using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCheckController : MonoBehaviour
{
    public bool isTouchingLedge;
    [SerializeField] private Transform _ledgeCheck;
    [SerializeField] private float _checkDistance;
    [SerializeField] private LayerMask _whatIsWall;

    private void FixedUpdate()
    {
        LedgeCheck();
    }
    private void LedgeCheck()
    {
        isTouchingLedge = Physics2D.Raycast(_ledgeCheck.position, transform.right, _checkDistance, _whatIsWall);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_ledgeCheck.position, new Vector2(_ledgeCheck.position.x + _checkDistance, _ledgeCheck.position.y));
    }
}
