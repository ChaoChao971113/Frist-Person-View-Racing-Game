using UnityEngine;

public class ShiftGravity : Gear
{
    public float shiftTime; // How long the gravity shift stays on before releasing
    private float currentShiftTime; // How long the gravity has been flipped on the player

    private Player playerScript;
    private Rigidbody playerRB;    
   
    private bool shiftGravity; // Boolean for whether the gravity is on or off

    private PostProcessing postProcessing;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = transform.parent.gameObject; // Save the parent that this item is attached to           
        playerRB = playerObject.GetComponent<Rigidbody>();// get the player rigid body    
        playerScript = playerObject.GetComponent<Player>(); // Get the player script from parent
        postProcessing = GameObject.FindGameObjectWithTag("PostProcessing").GetComponent<PostProcessing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shiftGravity)
        {           
            currentShiftTime += Time.deltaTime; // Track time since gravity is off            
            bool hits = playerScript.collideWith(); // invokes collideWith function from PlayerScript
            if ((currentShiftTime > shiftTime) || (hits)) //checking if the shift time is valid or collision with objects
            {
                ReturnGravity();
            }             
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    protected override void Activate()
    {
        // turn off gravity
        playerRB.useGravity = false;
        shiftGravity = true;
        currentCooldown = cooldown;
        currentShiftTime = 0;
        postProcessing.TimeWarpEffects(true);
    }

    // enable gravity and reset the gear
    private void ReturnGravity()
    {
        shiftGravity = false;
        playerRB.useGravity = true;
        postProcessing.TimeWarpEffects(false);
    }

    protected override bool ExtraConditions()
    {
        // Don't activate if the player is colliding with something
        return !playerScript.collideWith();
    }
}
