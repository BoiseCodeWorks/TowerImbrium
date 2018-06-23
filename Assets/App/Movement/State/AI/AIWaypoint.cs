using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaypoint : MonoBehaviour
{
    public Transform Destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIMotor>())
        {
            other.SendMessage("SetDestination", Destination);
        }
    }

}
