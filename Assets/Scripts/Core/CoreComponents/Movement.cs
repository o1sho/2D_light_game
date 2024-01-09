using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D Rigidbody { get; private set; }

    public int FacingDirection { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;


    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rigidbody.velocity;
    }

    #region Set Functions
    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void CheckItShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;
        Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
