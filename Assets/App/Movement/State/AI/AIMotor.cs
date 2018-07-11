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
    public AIWaypoint TargetDestination;
    public float distanceToTarget;

    private Animator anim;
    private NavMeshAgent agent;
    private bool Attacking = false;
    private Destroyable AttackTarget;
    private EnemyManager _manager;

    public void Init()
    {
        _manager = EnemyManager.instance;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.enabled = true;
        SetDestination(_manager.GetCurrentGoal());
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        SetDestination(_manager.GetCurrentGoal());
        distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        Attacking = false;
        if (distanceToTarget <= TargetDistance && AttackTarget != null)
        {
            Attacking = true;
            anim.SetFloat("Speed", 0);
            StartCoroutine("StartAttack");
            return;
        }
        if (Attacking) {
            return;
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

    public void SetDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }

    internal void SetAttackTarget(Destroyable destroyable)
    {
        AttackTarget = destroyable;
        agent.destination = destroyable.transform.position;
    }
}
