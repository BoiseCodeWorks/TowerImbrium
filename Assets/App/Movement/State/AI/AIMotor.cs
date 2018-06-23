using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMotor : MonoBehaviour
{
    private Vector3 destination = Vector3.zero;
    private Animator anim;

    private NavMeshAgent agent;

    public void init()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.enabled = true;
    }

    private void Update()
    {
        if (agent.isStopped)
        {
            anim.SetFloat("Speed", 0);
        }
        if (agent.hasPath)
        {
            anim.SetFloat("Speed", 1);
        }
    }


    public void SetDestination(Transform t)
    {
        agent.SetDestination(t.position);
    }

}
