using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector]
    public AudioManager AudioManager;
    [HideInInspector]
    public UIManager UIManager;
    [HideInInspector]
    public SpawnManager SpawnManager;
    [HideInInspector]
    public EnemyManager EnemyManager;
    [HideInInspector]
    public PlayerManager PlayerManager;

    public AudioClip StartMusic;

    private bool _paused = false;

    // Use this for initialization
    void Start()
    {
        AudioManager = AudioManager.instance;
        UIManager = UIManager.instance;
        SpawnManager = SpawnManager.instance;
        EnemyManager = EnemyManager.instance;
        PlayerManager = PlayerManager.instance;
        AudioManager.PlayMusicClip(StartMusic);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_paused)
            {
                _paused = false;
                UIManager.SetEventText("Wave " + SpawnManager.CurrentWaveNumber);
            }
            else
            {
                _paused = true;
                UIManager.SetEventText("PAUSED");
            }
            Time.timeScale = _paused ? 0 : 1;
        }
    }

    public void UpdateHealth(float maxHealth, float currentHealth)
    {
        float percent = (currentHealth / maxHealth) * 100;
        UIManager.AdjustHealthBar(percent * .01f);
    }

}
