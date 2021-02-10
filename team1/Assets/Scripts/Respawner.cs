using UnityEngine;

public class Respawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Respawn respawnable = collision.collider.GetComponent<Respawn>();
        if (respawnable != null)
        {
            respawnable.RespawnAtCheckpoint();
        }
    }
}
