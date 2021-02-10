using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Use this to set checkpoints
    [SerializeField]
    private Vector3 respawnPoint;
    // The restart position is just where the player initally spawns
    private Vector3 startPoint;
    private Rigidbody rb;


    private void Start()
    {
        startPoint = transform.position;
        respawnPoint = startPoint;
        rb = GetComponent<Rigidbody>();
    }

    public void RespawnAtCheckpoint()
    {
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void RespawnAtStart()
    {
        transform.TransformPoint(startPoint);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void SetCheckpoint(Vector3 checkpoint)
    {
        respawnPoint = checkpoint;
    }
}
