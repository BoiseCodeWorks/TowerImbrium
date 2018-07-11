using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == other.tag) { return; }
        var destroyable = other.GetComponent<Destroyable>();
        if (destroyable)
        {
            SendMessageUpwards("OnHitDestroyable", destroyable);
        }
    }

}
