using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms

    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }
    public Transform CeilingCheck { get => ceilingCheck; private set => ceilingCheck = value; }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform entityCheckMin;
    [SerializeField] private Transform entityCheckMax;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float entityCheckMaxDistance;
    [SerializeField] private float entityCheckMinDistance;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsEntity;

    #endregion

    #region Check Functions

    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(ceilingCheck.position, GroundCheckRadius, WhatIsGround);
    }
    public bool Ground
    {
        get => Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, WhatIsGround);
    }

    public bool Wall
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, WallCheckDistance, WhatIsGround);
    }

    public bool Ledge
    {
        get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, WallCheckDistance, WhatIsGround);
    }

    public bool EntityMax
    {
        get => Physics2D.Raycast(entityCheckMax.position, Vector2.right * core.Movement.FacingDirection, entityCheckMaxDistance, whatIsEntity);
    }

    public bool EntityMin
    {
        get => Physics2D.Raycast(entityCheckMin.position, Vector2.right * core.Movement.FacingDirection, entityCheckMinDistance, whatIsEntity);
    }


    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, GroundCheckRadius);
        Gizmos.DrawLine(ledgeCheck.position, new Vector2(ledgeCheck.position.x + WallCheckDistance, ledgeCheck.position.y));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + WallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(ceilingCheck.position, GroundCheckRadius);

        Gizmos.DrawLine(entityCheckMin.position, new Vector2(entityCheckMin.position.x + entityCheckMinDistance, entityCheckMin.position.y));
        Gizmos.DrawLine(entityCheckMax.position, new Vector2(entityCheckMax.position.x + entityCheckMaxDistance, entityCheckMax.position.y));
    }
    #endregion
}
