using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalkState : WalkState
{
    public override void Transition()
    {
        if (!motor.Grounded())
        {
            motor.ChangeState("AIFallState");
        }
    }
}
