using UnityEngine;

// Pedro's extra jump, adapted by Jack
public class ExtraJump : Gear
{
    public float jumpHeight = 800; // amount of height  the player will be able to jump

    private Vector3 jumpDirection; // Vector representing the direction we will jump in
    private Vector3 contactSurfaceNormal; // The normal of the surface the player is last in contact with

    private Player playerScript;
    private Rigidbody playerRB;


    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject; // the owner of the extraJump
        playerObject = transform.parent.gameObject; // Save the parent that this item is attached to
        playerScript = playerObject.GetComponent<Player>(); // Get the player script from parent
        playerRB = playerObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
    }

    protected override void Activate()
    {
        jumpDirection = Vector3.Lerp(playerScript.GetContactSurfaceNormal(), Vector3.up, playerScript.jumpUpwardsBias); // Lerp jump direction towards player's up
        playerRB.AddForce(jumpDirection * jumpHeight); // Add force in the direction of the jump
        currentCooldown = cooldown;
    }
}