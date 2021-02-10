using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }
    public Player player;

    [SerializeField]
    private Checkpoint[] checkpoints;

    private void Start()
    {
        if (GameManager.Instance.zoneMode)
        {
            // The checkpoint is actually the spawn point + 1 (because the start isn't a checkpoint)
            if (GameManager.Instance.checkPoint > 0)
            {
                player.transform.position = checkpoints[GameManager.Instance.checkPoint - 1].transform.position;
                Debug.Log("Setting position to: " + GameManager.Instance.checkPoint);
            }
            checkpoints[GameManager.Instance.checkPoint].IsFinishLine = true;
        }
        else
        {
            checkpoints[checkpoints.Length - 1].IsFinishLine = true;
        }
    }

    void Awake()
    {
        if (Instance == null) // Level manager not created yet
        {
            Instance = this;
        }
    }

    public bool IsLastCheckpoint()
    {
        // Check if we started at the second last checkpoint (the last is the overall finish line)
        return GameManager.Instance.checkPoint + 1 == checkpoints.Length;
    }
}
