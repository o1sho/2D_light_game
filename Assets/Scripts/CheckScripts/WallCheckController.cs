using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckController : MonoBehaviour
{
    public bool isTouchingWall;
    [SerializeField] private float _checkDistance;
    [SerializeField] private Transform _wallCheck;

    [SerializeField] private LayerMask _whatIsWall;

    private void FixedUpdate()
    {
        WallCheck();
    }

    private void WallCheck()
    {
        isTouchingWall = Physics2D.Raycast(_wallCheck.position, transform.right, _checkDistance, _whatIsWall);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_wallCheck.position, new Vector2(_wallCheck.position.x + _checkDistance, _wallCheck.position.y));
    }
}
