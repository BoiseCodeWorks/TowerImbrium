using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class BaseMotor : MonoBehaviour
{
    protected CharacterController controller;
    protected Transform characterTransform;
    protected BaseState State;

    private float baseSpeed = 5;
    private float baseGravity = 15;
    private float baseJumpForce = 7;
    private float terminalVelocity = 30;
    private float distanceToGround = .5f;
    private float groundRayOffset = .1f;

    public float Speed { get { return baseSpeed; } }
    public float Gravity { get { return baseGravity; } }
    public float TerminalVelocity { get { return terminalVelocity; } }
    public float JumpForce { get { return baseJumpForce; } }

    public float VerticalVelocity { get; set; }
    public Vector3 MoveVector { get; set; }
    public Quaternion RotationQuaternion { get; set; }

    protected virtual void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        characterTransform = transform;
    }

    protected void Update()
    {
        UpdateMotor();
    }

    protected virtual void Rotate()
    {
        characterTransform.rotation = RotationQuaternion;
    }

    protected virtual void Move()
    {
        controller.Move(MoveVector * Time.deltaTime);
    }

    public void ChangeState(string stateName)
    {
        var t = System.Type.GetType(stateName);
        if (t != null)
        {
            Debug.Log("Transitioning to new State " + stateName);
            State.Teardown();
            State = gameObject.AddComponent(t) as BaseState;
            State.Init();
        }
        else
        {
            Debug.Log("[Invalid State Transition] State not found " + stateName);
        }
    }

    public virtual bool Grounded()
    {
        RaycastHit hit;
        Vector3 ray;
        var extents = controller.bounds.extents;
        var center = controller.bounds.center;
        float y = (center.y - extents.y) + .3f;
        float x = extents.x - groundRayOffset;
        float z = extents.z - groundRayOffset;

        ray = new Vector3(center.x, y, center.z);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, distanceToGround)) { return true; }

        ray = new Vector3(center.x + x, y, center.z + z);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, distanceToGround)) { return true; }

        ray = new Vector3(center.x - x, y, center.z + z);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, distanceToGround)) { return true; }

        ray = new Vector3(center.x + x, y, center.z - z);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, distanceToGround)) { return true; }
        return false;
    }

    protected abstract void UpdateMotor();
}
