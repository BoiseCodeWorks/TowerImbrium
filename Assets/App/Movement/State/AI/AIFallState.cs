using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFallState : FallState
{
    public override void Transition()
    {
        if (motor.Grounded())
        {
            motor.VerticalVelocity = 0;
            motor.ChangeState("AIWalkState");
        }
    }
}
