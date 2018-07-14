using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{

    public float MaxHealth = 100;
    Destroyable _destroyable;
    // Use this for initialization
    void Start()
    {
        _destroyable = GetComponent<Destroyable>();
        if (_destroyable != null)
        {
            _destroyable.Health = MaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_destroyable == null)
        {
            _destroyable = GetComponent<Destroyable>();
            _destroyable.Health = MaxHealth;
        }
        GameManager.instance.UpdateHealth(MaxHealth, _destroyable.Health);
    }
}
