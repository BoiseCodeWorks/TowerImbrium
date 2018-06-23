using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaypoint : MonoBehaviour
{
    public AIWaypoint Destination;

    private void OnTriggerEnter(Collider other)
    {
        var AI = other.GetComponent<AIMotor>();
        if (AI != null)
        {
            AI.SetDestination(Destination);
        }
    }

}
