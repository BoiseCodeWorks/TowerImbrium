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
        if(Destroyed)
        {
            OnDestroy();
        }
    }

    public virtual void AdjustDefenseModifier(float n)
    {
        DefenseModifier += n;
    }

    public virtual void OnDestroy()
    {
        anim.SetTrigger("Destroyed");
        ps.Play();
        Destroy(gameObject, DeathAnimationDuration);
    }
}
