using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState
{
    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplySpeed(ref input, motor.Speed);
        return input;
    }

    public override Quaternion ProcessRotation(Vector3 input)
    {
        return Quaternion.FromToRotation(Vector3.forward, input);
    }

    public override void Transition()
    {
        if (!motor.Grounded())
        {
            motor.ChangeState("FallState");
        }
        if (Input.GetButtonDown("Jump"))
        {
            motor.ChangeState("JumpState");
        }
    }
}
