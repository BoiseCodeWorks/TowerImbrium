using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : BaseState
{
    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplySpeed(ref input, motor.Speed);
        ApplyGravity(ref input, motor.Gravity);
        return input;
    }
    public override void Transition()
    {
        if (motor.Grounded())
        {
            motor.VerticalVelocity = 0;
            motor.ChangeState("WalkState");
        }
    }
}
