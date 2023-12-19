using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckController : MonoBehaviour
{
    [Header("WALL CHECK SETTINGS:")]
    public bool isTouchingWall;
    public float _checkDistance;
    [SerializeField] private Transform _wallCheck;

    [Header("LEDGE CHECK SETTINGS:")]
    public bool isTouchingLedge;
    [SerializeField] private Transform _ledgeCheck;
    public bool ledgeDetected;
    public Vector2 _ledgePosBot;

    [SerializeField] private LayerMask _whatIsWall;


    private void Update()
    {
        WallCheck();
        LedgeCheck();
    }
    private void FixedUpdate()
    {
    }

    private void WallCheck()
    {
        isTouchingWall = Physics2D.Raycast(_wallCheck.position, transform.right, _checkDistance, _whatIsWall);
    }
    private void LedgeCheck()
    {
        isTouchingLedge = Physics2D.Raycast(_ledgeCheck.position, transform.right, _checkDistance, _whatIsWall);
        if(isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected= true;
            _ledgePosBot = _wallCheck.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_wallCheck.position, new Vector2(_wallCheck.position.x + _checkDistance, _wallCheck.position.y));
        Gizmos.DrawLine(_ledgeCheck.position, new Vector2(_ledgeCheck.position.x + _checkDistance, _ledgeCheck.position.y));
    }
}
