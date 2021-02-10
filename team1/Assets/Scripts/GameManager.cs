using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Set this to choose which gear the player spawns with")]
    public GameObject gear1;
    public GameObject gear2;

    [Header("Set this to choose which level the player will play in")]
    public int level;

    [Header("Set this to choose which checkpoint the player spawns from")]
    public int checkPoint;

    [Header("Set this to choose whether the level will be broken up by the checkpoints")]
    public bool zoneMode;
    
    public static GameManager Instance { get; private set; }

    public void SetGear(GameObject newGear)
    {
        gear1 = newGear;
    }

    public void SetGear2(GameObject newGear)
    {
        gear2 = newGear;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    public void SetCheckpoint(int newCheckpoint)
    {
        checkPoint = newCheckpoint;
    }

    void Awake()
    {
        if(Instance == null) // game manager not created yet
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // find the player in the scene
        if (player != null) // Player has been found, assign the selected gear to it
        {
            Player playerScript = player.GetComponent<Player>();

            if (gear1 != null) // If a gear item has been selected
            {
                GameObject playerGear = Instantiate(gear1); // Create the gear
                playerGear.transform.parent = player.transform; // attach to player

                playerScript.EquipGear1(playerGear.GetComponent<Gear>()); // assign the gear to the player script
            }
            if (gear2 != null) // If a gear item has been selected
            {
                GameObject playerGear = Instantiate(gear2); // Create the gear
                playerGear.transform.parent = player.transform; // attach to player

                playerScript.EquipGear2(playerGear.GetComponent<Gear>()); // assign the gear to the player script
            }
        }
    }
}