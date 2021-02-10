using UnityEngine;

public class Jetpack : Gear
{
    public float continuousThrustStrength; // The strength of the jetpack when using the primary continuous thrust 
    public float maxFuel; // The amount of fuel the jetpack can hold
    public float fuelRegenRate; // How quickly the jetpack's fuel regenerates
    public float fuelToActivate; // The amount of fuel used immediately upon activation of jetpack (this prevents player from quickly starting and stopping repeatedly)
    public float fuelBurnRate; // The rate at which fuel is used when burning continuously

    private Player playerScript;
    private Rigidbody playerRB;

    private float currentFuel; // The amount of fuel available for use
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = transform.parent.gameObject; // Save the parent that this item is attached to
        playerScript = playerObject.GetComponent<Player>(); // Get the player script from parent
        playerRB = playerObject.GetComponent<Rigidbody>();

        currentFuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) // if the gear has been activated successfully
        {
            if(Input.GetMouseButton(1)) // if the mouse is still being held
            {
                Debug.Log(currentFuel);
                playerRB.AddForce(Vector3.up * continuousThrustStrength * Time.deltaTime); // Add upwards force based on the jetpack's strength

                currentFuel -= fuelBurnRate * Time.deltaTime;
                if (currentFuel < 0) // Out of fuel
                {
                    active = false;
                    currentFuel = 0;
                }
            }
            else // mouse released
            {
                active = false;
            }
        }else if(currentFuel < maxFuel) // Regenerate fuel every tick until full
        {
            currentFuel += fuelRegenRate * Time.deltaTime;
        }
    }

    protected override void Activate()
    {
        Debug.Log("Activated");
        currentFuel -= fuelToActivate;
        active = true;
    }

    protected override bool ExtraConditions()
    {
        // Make sure the player has enough fuel to activate the jetpack
        return currentFuel > fuelToActivate;
    }
}
