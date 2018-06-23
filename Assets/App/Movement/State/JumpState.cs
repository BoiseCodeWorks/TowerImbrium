using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    public override void Init()
    {
        base.Init();
        motor.VerticalVelocity = motor.JumpForce;
    }
    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplySpeed(ref input, motor.Speed);
        ApplyGravity(ref input, motor.Gravity);
        return input;
    }

    public override void Transition()
    {
        if(motor.VerticalVelocity < 0)
        {
            motor.ChangeState("FallState");
        }
    }
}
