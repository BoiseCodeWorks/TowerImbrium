using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggersController : MonoBehaviour
{
    public float Damage = 25f;
    public float AttackPushForce = 15f;
    public List<AttackTrigger> Triggers = new List<AttackTrigger>();
    public List<AudioClip> Sounds = new List<AudioClip>();
    public SphereCollider KnockbackZone;

    private void Awake()
    {
        DisableTriggers();
        if(KnockbackZone != null)
        {
            KnockbackZone.enabled = false;
        }
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
        if (item.gameObject == gameObject) { return; }

        AudioManager.instance.RandomizeSfx(Sounds);
        item.ApplyDamage(Damage);
        if (item.KnockbackEnabled && KnockbackZone != null)
        {
            StartCoroutine(ApplyKnockback());
        }
    }

    private IEnumerator ApplyKnockback()
    {
        KnockbackZone.enabled = true;
        yield return new WaitForSeconds(.35f);
        KnockbackZone.enabled = false;
    }
}
