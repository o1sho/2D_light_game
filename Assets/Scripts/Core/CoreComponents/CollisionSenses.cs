using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms

    public Transform GroundCheck 
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    public Transform WallCheck 
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }
    public Transform LedgeCheckHorizontal 
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    public Transform LedgeCheckVertical 
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }
    public Transform CeilingCheck 
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }
    public Transform EntityCheckMin 
    {
        get => GenericNotImplementedError<Transform>.TryGet(entityCheckMin, core.transform.parent.name);
        private set => entityCheckMin = value;
    }
    public Transform EntityCheckMax 
    {
        get => GenericNotImplementedError<Transform>.TryGet(entityCheckMax, core.transform.parent.name);
        private set => entityCheckMax = value;
    }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public float EntityCheckMinDistance { get => entityCheckMinDistance; set => entityCheckMinDistance = value; }
    public float EntityCheckMaxDistance { get => entityCheckMaxDistance; set => entityCheckMaxDistance = value; }

    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
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
        get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool Wall
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    }

    public bool EntityMax
    {
        get => Physics2D.Raycast(EntityCheckMax.position, Vector2.right * core.Movement.FacingDirection, entityCheckMaxDistance, whatIsEntity);
    }

    public bool EntityMin
    {
        get => Physics2D.Raycast(EntityCheckMin.position, Vector2.right * core.Movement.FacingDirection, entityCheckMinDistance, whatIsEntity);
    }


    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(ledgeCheckHorizontal.position, new Vector2(ledgeCheckHorizontal.position.x + wallCheckDistance, ledgeCheckHorizontal.position.y));
        Gizmos.DrawLine(ledgeCheckVertical.position, new Vector2(ledgeCheckVertical.position.x, ledgeCheckVertical.position.y - wallCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(ceilingCheck.position, groundCheckRadius);

        Gizmos.DrawLine(entityCheckMin.position, new Vector2(entityCheckMin.position.x + entityCheckMinDistance, entityCheckMin.position.y));
        Gizmos.DrawLine(entityCheckMax.position, new Vector2(entityCheckMax.position.x + entityCheckMaxDistance, entityCheckMax.position.y));
    }
    #endregion
}
