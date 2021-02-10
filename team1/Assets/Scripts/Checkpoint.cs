using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool IsFinishLine {get; set;}

    // This is the respawn position associated with this checkpoint
    [SerializeField]
    private Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        Respawn respawnable = other.GetComponent<Respawn>();
        if (respawnable != null)
        {
            respawnable.SetCheckpoint(checkpoint.position);
        }
        if (IsFinishLine)
        {
            EndSection player = other.GetComponent<EndSection>();
            if (player != null)
            {
                player.FinishSection();
            }
        }
    }
}
