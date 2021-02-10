using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The surf gear will allow the player to have more precise control and maintain more speed when turning 
public class Surf : Gear
{
    public GameObject playerObject; // The player who this grapple belongs to
    public float TurningMultiplier; // How much faster the player turns
    public float BreakingMultiplier; // How much more the player retains speed when turning

    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = transform.parent.gameObject; // Save the parent that this item is attached to
        playerScript = playerObject.GetComponent<Player>(); // Get the player script from parent

        playerScript.turnSpeed *= TurningMultiplier;
        playerScript.breakThreashHold *= BreakingMultiplier;
    }
}