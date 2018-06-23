using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseMotor))]
public abstract class BaseState : MonoBehaviour
{
    protected BaseMotor motor;
    public abstract Vector3 ProcessMotion(Vector3 input);

    public virtual void Init()
    {
        motor = GetComponent<BaseMotor>();
    }

    public virtual void Teardown()
    {
        Destroy(this);
    }

    protected void ApplySpeed(ref Vector3 input, float speed)
    {
        input *= speed;
    }

    protected void ApplyGravity(ref Vector3 input, float gravity)
    {
        motor.VerticalVelocity -= gravity * Time.deltaTime;
        motor.VerticalVelocity = Mathf.Clamp(motor.VerticalVelocity,
                                            -motor.TerminalVelocity,
                                            motor.TerminalVelocity);
        input.Set(input.x, motor.VerticalVelocity, input.z);
    }

    public virtual Quaternion ProcessRotation(Vector3 input)
    {
        return transform.rotation;
    }

    public abstract void Transition();

}
