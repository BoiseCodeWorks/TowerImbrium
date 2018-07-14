using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(ParticleSystem))]
public class Destroyable : MonoBehaviour
{
    public float Health = 100;
    public float DefenseModifier = 1;
    public float DeathAnimationDuration = 1.75f;
    public bool PlayAnimationOnDestroy = false;
    public bool Destroyed { get { return Health <= 0; } }
    public bool KnockbackEnabled = true;
    public List<AudioClip> DamageSounds;
    public List<AudioClip> DeathSounds;

    private Animator anim;
    private ParticleSystem ps;

    private void Start()
    {
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    public virtual void ApplyDamage(float damage)
    {
        Health -= damage * DefenseModifier;
        if (Destroyed)
        {
            RunDestroy();
            return;
        }
        if (DamageSounds != null)
        {
            AudioManager.instance.RandomizeSfx(DamageSounds);
        }
    }

    public virtual void AdjustDefenseModifier(float n)
    {
        DefenseModifier += n;
    }

    public void RunDestroy()
    {
        anim.SetTrigger("Destroyed");
        ps.Play();
        if (DamageSounds != null)
        {
            AudioManager.instance.RandomizeSfx(DeathSounds);
        }
        Destroy(gameObject, DeathAnimationDuration);
    }
}
