using UnityEngine;

public class Grapple : Gear
{
    public float pullSpeed; // The speed at which the grapple will pull the player in
    public float attachTime; // How long the grapple stays attached to a point before releasing
    private float currentAttachTime; // How long the grapple has been attached for

    private Player playerScript;
    private Rigidbody playerRB;
    private Transform playerTR;
    private Camera playerCam;
    private LineRenderer lineRenderer;

    private RaycastHit hit; // Point that the grapple has raycasted
    private bool attached; // Boolean for whether the grapple is currently attached to something

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = transform.parent.gameObject; // Save the parent that this item is attached to
        playerScript = playerObject.GetComponent<Player>(); // Get the player script from parent
        playerRB = playerObject.GetComponent<Rigidbody>();
        playerTR = playerObject.transform;
        playerCam = playerScript.cam;

        lineRenderer = GetComponent<LineRenderer>(); // Get the line renderer on this grapple object
    }

    void Update()
    {
        if (attached)
        {
            lineRenderer.SetPosition(0, playerTR.position); // Set line start to player's current position
            Vector3 playerToAttachPoint = (hit.point - playerTR.position).normalized; // find direction to attached point from player
            playerRB.AddForce(playerToAttachPoint * pullSpeed); // Pull player towards attached point

            currentAttachTime += Time.deltaTime; // Track time since attached

            if (currentAttachTime > attachTime)
            {
                Detatch();
            }
        }

        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;
    }

    protected override void Activate()
    {
        attached = true;
        lineRenderer.enabled = true; // Enable line renderer
        lineRenderer.SetPosition(1, hit.point); // Set line end to hit point
        currentCooldown = cooldown;
        currentAttachTime = 0;
    }

    private void Detatch()
    {
        attached = false;
        currentAttachTime = 0;
        lineRenderer.enabled = false; // Disable line renderer when not attached
    }

    protected override bool ExtraConditions()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)); // Ray through middle of screen
        return Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Ramp");
    }
}