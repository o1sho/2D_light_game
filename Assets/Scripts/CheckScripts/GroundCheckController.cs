using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundCheckController : MonoBehaviour
{
    public bool isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;

    private void Update()
    {
        GroundCheck();
    }
    private void FixedUpdate()
    {

    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}
