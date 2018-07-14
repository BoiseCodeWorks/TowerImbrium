using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnWave
{
    [Header("Wave Details")]
    [Space(15f)]
    [Header("Require all prefabs to be cleared before progressing waves.")]
    public bool ClearBeforeAdvancing = true;
    [Tooltip("Delays the start of the wave in seconds")]
    [Range(0f, 180f)]
    public float WaveDelay = 8f;
    public bool WaveCleared { get; private set; }

    [Space(15f)]
    [Header("Wave Spawn Configuration")]
    public List<SpawnPoint> SpawnPoints;
    public List<SpawnablePrefab> Spawnables;

    [Tooltip("Default behavior will cycle through all spawn points.")]
    public bool RandomizeSpawnPoints;
    [Range(.25f, 3f)]
    public float SpawnRateInterval = .5f;

    private int _spawnIndex = -1;

    private bool ValidateWave()
    {
        List<string> errors = new List<string>();
        if (Spawnables.Count == 0)
        {
            errors.Add("[WAVE ERROR] Unable to prepare wave. No SpawnPoints were set");
        }
        if (SpawnPoints.Count == 0)
        {
            errors.Add("[WAVE ERROR] Unable to prepare wave. No SpawnPoints were set");
        }
        bool validWave = errors.Count == 0;
        if (!validWave)
        {
            errors.ForEach(e => Debug.LogError(e));
        }
        return validWave;
    }

    public bool PrepareWave()
    {
        //Safety Check for Wave
        if (!ValidateWave()) { return false; }

        foreach (var spawnable in Spawnables)
        {
            if (spawnable.SpawnPoint == null)
            {
                spawnable.SpawnPoint = GetSpawnPoint();
            }
        }
        return true;
    }

    public SpawnPoint GetSpawnPoint()
    {
        if (RandomizeSpawnPoints)
        {
            return SpawnPoints[Random.Range(0, SpawnPoints.Count - 1)];
        }
        //Cycles through each spawn point
        _spawnIndex += 1;
        if (_spawnIndex > SpawnPoints.Count - 1)
        {
            _spawnIndex = 0;
        }
        return SpawnPoints[_spawnIndex];
    }

    public void ClearWave()
    {
        WaveCleared = true;
        //stretch goal implement score on wave completion
    }
}

[System.Serializable]
public class SpawnablePrefab
{
    public GameObject Prefab;
    public int Quantity = 1;
    [Tooltip("Will be auto assigned if not set")]
    public SpawnPoint SpawnPoint;

    public void OnSpawn(GameObject go)
    {
        go.SendMessage("Init");
    }

}

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public List<SpawnWave> Waves = new List<SpawnWave>();

    public List<GameObject> _activeGameObjects = new List<GameObject>();
    private SpawnWave _currentWave;

    private int waveIndex = -1;
    public int CurrentWaveNumber { get { return waveIndex + 1; } }

    private void Start()
    {
        _currentWave = Waves.Count > 0 ? Waves[0] : null;
        if (_currentWave == null)
        {
            Debug.LogWarning("[INVALID SPAWN MANAGER] Add Waves to use SpawnManager");
            return;
        }
        waveIndex = 0;
        StartWave();
    }

    public IEnumerator StartSpawn()
    {
        //Spawn each item in the List by Spawn Rate Interval and quantity
        foreach (var spawnable in _currentWave.Spawnables)
        {
            for (var i = 0; i < spawnable.Quantity; i++)
            {
                SpawnPrefab(spawnable);
                yield return new WaitForSeconds(_currentWave.SpawnRateInterval);
            }
        }
        yield return true;
    }

    public IEnumerator BeginWave()
    {
        UIManager.instance.SetEventText("INCOMING....");
        yield return new WaitForSeconds(_currentWave.WaveDelay);
        UIManager.instance.SetEventText("Wave " + CurrentWaveNumber);
        StartCoroutine(StartSpawn());
    }

    private void SpawnPrefab(SpawnablePrefab spawnable)
    {
        if(spawnable.SpawnPoint == null)
        {
            spawnable.SpawnPoint = _currentWave.GetSpawnPoint();
        }
        var go = Instantiate(spawnable.Prefab, spawnable.SpawnPoint.transform.position, spawnable.SpawnPoint.transform.rotation);
        spawnable.OnSpawn(go);
        _activeGameObjects.Add(go);
    }

    public void AdvanceWave()
    {
        //Check to see if the current wave can be skipped
        if (_currentWave.ClearBeforeAdvancing && !_currentWave.WaveCleared)
        {
            UIManager.instance.SetEventText("Unable to advance, wave still in progress");
            return;
        }

        //Forcefully removes any existing objects in current wave prior to advancing
        foreach (var go in _activeGameObjects)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
        //Clears the list of game objects
        _activeGameObjects.Clear();

        //Marks the wave as completed
        _currentWave.ClearWave();

        //Set next wave if there is one
        if (waveIndex < Waves.Count - 1)
        {
            waveIndex += 1;
            _currentWave = Waves[waveIndex];
            StartWave();
        }
        else
        {
            // All Waves Cleared
            UIManager.instance.SetEventText("All Waves Cleared");
        }
    }

    private void StartWave()
    {
        if (_currentWave.PrepareWave())
        {
            StartCoroutine(BeginWave());
        }
        else
        {
            Debug.LogWarning("[WAVE WARNING] Skipped Wave " + waveIndex + " bad configuration");
            AdvanceWave();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            AdvanceWave();
        }
    }


}
