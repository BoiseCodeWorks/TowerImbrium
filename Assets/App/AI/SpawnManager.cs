using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoint
{
    public Transform self;
    public AIWaypoint destination;
}

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();
    public List<GameObject> SpawnPrefabs = new List<GameObject>();

    [SerializeField]
    private int currentSpanwPoint = 0;

    void Spawn(int prefabIndex, int spawnIndex)
    {
        var point = SpawnPoints[spawnIndex];
        var prefab = SpawnPrefabs[prefabIndex];
        var go = Instantiate(prefab, point.self.position, point.self.rotation);
        var motor = go.GetComponent<AIMotor>();
        motor.init();
        motor.SetDestination(point.destination);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentSpanwPoint++;
            if (currentSpanwPoint >= SpawnPoints.Count)
            {
                currentSpanwPoint = 0;
            }
            Spawn(0, currentSpanwPoint);
        }
    }


}
