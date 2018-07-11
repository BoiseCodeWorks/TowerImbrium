using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager> {

    public List<AIWaypoint> EnemyGoals;

    public Vector3 GetCurrentGoal()
    {
        return EnemyGoals.Find(p => p.isActiveAndEnabled).transform.position;
    }
	
}
