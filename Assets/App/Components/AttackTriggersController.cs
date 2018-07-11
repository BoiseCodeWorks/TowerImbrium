using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggersController : MonoBehaviour
{
    public float Damage = 25f;
    public List<AttackTrigger> Triggers;

    private void Awake()
    {
        DisableTriggers();
    }

    public void EnableTriggers()
    {
        Triggers.ForEach(t => t.gameObject.SetActive(true));
    }

    public void DisableTriggers()
    {
        Triggers.ForEach(t => t.gameObject.SetActive(false));
    }

    public void OnHitDestroyable(Destroyable item)
    {
        if(item.gameObject == gameObject) { Debug.Log("HIT SELF"); return; }

        Debug.Log("HIT:" + item.name);
        item.ApplyDamage(Damage);
    }

}
