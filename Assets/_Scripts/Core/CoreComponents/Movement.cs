using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D Rigidbody { get; private set; }

    public int FacingDirection { get; private set; }

    public bool CanSetVelocity { get; set; }

    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;


    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = Rigidbody.velocity;
    }

    #region Set Functions
    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetVelocity(Vector2 angle, float velocity, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        //Rigidbody.velocity = workspace;
        //CurrentVelocity = workspace;
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        //Rigidbody.velocity = workspace;
        //CurrentVelocity = workspace;
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            Rigidbody.velocity = workspace;
            CurrentVelocity = workspace;
        }
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
