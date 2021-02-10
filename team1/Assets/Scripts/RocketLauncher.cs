using UnityEngine;

public class RocketLauncher : Gear
{
    public GameObject rocket; // The rocket that is fired by the launcher
    public float rocketSpeed;
    public float explosionStrength;

    private Camera playerCam;
    private Player playerScript;
    private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = transform.parent.gameObject; // Save the parent that this item is attached to
        playerScript = playerObject.GetComponent<Player>(); // Get the player script from parent
        playerRB = playerObject.GetComponent<Rigidbody>();
        playerCam = playerObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;

    }

    protected override void Activate()
    {
        GameObject rocketOBJ = Instantiate(rocket); // Create a new rocket
        rocketOBJ.transform.position = transform.position;
        Rigidbody rocketRB = rocketOBJ.GetComponent<Rigidbody>();
        Rocket rocketScript = rocketOBJ.GetComponent<Rocket>();

        rocketRB.velocity = playerCam.transform.forward * rocketSpeed + playerRB.velocity; // set the velocity of the spawned rocket
        rocketScript.playerRB = playerRB; // pass the player's rb to the rocket
        rocketScript.explosionStrength = explosionStrength;
        currentCooldown = cooldown; // set the cooldown
    }
}
