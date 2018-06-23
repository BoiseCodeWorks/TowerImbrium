using Assets.App.Camera;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : BaseMotor
{
    public ThirdPersonCameraController camMotor;
    private Transform camTransform;

    protected override void Start()
    {
        base.Start();
        State = gameObject.AddComponent<WalkState>();
        State.Init();
        camTransform = camMotor.transform;
    }

    private Vector3 GetInputDirection()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if(dir.magnitude > 1)
        {
            dir.Normalize();
        }

        return dir;
    }

    private Vector3 RotateWithView(Vector3 input)
    {
        Vector3 dir = camTransform.TransformDirection(input);
        dir.Set(dir.x, 0, dir.z);
        return dir.normalized * input.magnitude;
    }

    protected override void UpdateMotor()
    {
        MoveVector = GetInputDirection();
        MoveVector = RotateWithView(MoveVector);
        MoveVector = State.ProcessMotion(MoveVector);
        RotationQuaternion = State.ProcessRotation(MoveVector);
        State.Transition();
        Move();
        Rotate();
    }
}
