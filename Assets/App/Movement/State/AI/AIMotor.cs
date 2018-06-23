using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(AISight))]
[RequireComponent(typeof(Destroyable))]
public class AIMotor : MonoBehaviour
{
    public float TargetDistance = .3f;
    public float AttackCooldown = 1.5f;
    public AIWaypoint NextWaypoint;
    public float distanceToTarget;

    private Animator anim;
    private NavMeshAgent agent;
    private bool Attacking = false;
    private Destroyable AttackTarget;

    public void init()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.enabled = true;
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        if (distanceToTarget <= TargetDistance && AttackTarget != null)
        {
            Attacking = true;
            anim.SetFloat("Speed", 0);
            StartCoroutine("StartAttack");
        }
        if (Attacking) { return; }
        if (agent.isStopped)
        {
            anim.SetFloat("Speed", 0);
        }
        if (agent.hasPath)
        {
            anim.SetFloat("Speed", 1);
        }
    }

    IEnumerator StartAttack()
    {
        anim.SetBool("StartAttack", Attacking);
        yield return new WaitForSeconds(AttackCooldown);
        Attacking = false;
        anim.SetBool("StartAttack", Attacking);
    }

    public void SetDestination(AIWaypoint waypoint)
    {
        NextWaypoint = waypoint;
        agent.SetDestination(waypoint.transform.position);
    }

    public void SetDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void MoveToWaypoint()
    {
        SetDestination(NextWaypoint);
    }

    internal void SetAttackTarget(Destroyable destroyable)
    {
        AttackTarget = destroyable;
        agent.destination = destroyable.transform.position;
    }
}
