using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing_kill : MonoBehaviour
{
    


    private void OnParticleCollision(GameObject other)
    {
        Respawn respawnable = other.GetComponent<Respawn>();
        if (respawnable != null)
        {
            respawnable.RespawnAtCheckpoint();
        }

    }
}
