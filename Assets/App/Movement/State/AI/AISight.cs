using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider), typeof(Animator))]
[RequireComponent(typeof(AIMotor))]
public class AISight : MonoBehaviour
{

    public float FieldOfViewAngle = 110;
    public bool PlayerInSight = false;
    public Vector3 PlayerLastSighting;

    private AIMotor motor;
    private SphereCollider col;
    private Animator anim;
    private Vector3 LastPlayerSighting;
    private GameObject Player;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        motor = GetComponent<AIMotor>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == gameObject.tag) { return; }
        if (other.GetComponent<PlayerMotor>())
        {
            Player = other.gameObject;
            PlayerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < FieldOfViewAngle * .5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject == Player)
                    {
                        PlayerInSight = true;
                        LastPlayerSighting = Player.transform.position;
                        motor.SetDestination(LastPlayerSighting);
                        motor.SetAttackTarget(Player.GetComponent<Destroyable>());
                        return;
                    }
                }
            }
        }
        if (other.GetComponent<Destroyable>())
        {
            motor.SetAttackTarget(other.GetComponent<Destroyable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMotor>())
        {
            PlayerInSight = false;
        }
    }


}
