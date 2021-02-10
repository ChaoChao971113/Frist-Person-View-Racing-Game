using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rB;
    Transform tr;
    CollisionList collisionListScript;

    public float mouseSensitivity;
    public float bootAcceleration; // How fast your boots accelerate 
    [Range(1, 200)] public float inputSpeedFalloff; // How intense the falloff is for input as player moves faster
    [Range(0, 10)] public float turnSpeed; // How fast the player can redirect velocity
    [Range(0, 1)] public float wallRunAbility; // How well the you can hold onto walls
    public int jumpPower;
    [Range(0, 1)] public float jumpUpwardsBias; // At 1, jumping is always straight up, at 0, jump direction is the normal of the surface the player is on
    [Range(0, 1)] public float breakThreashHold; // How far the player can turn without losing speed, higher retains more speed
    [Header("Player Settings")]
    [Range(-1, 1)] public int mouseInversion;
    public float breakingRange; // How far the player can try to turn without losing speed

    public Gear gear1;
    public Gear gear2;


    // Player Input
    private float hInput; // AD
    private float vInput; // WS
    private bool jumpInput; // Space pressed this frame
    private float timeSinceJumpInput;
    private Vector3 normalizedInput;
    private float mouseX;
    private float mouseY;

    private Vector3 jumpDirection; // Vector representing the direction we will jump in
    private Vector3 contactSurfaceNormal; // The normal of the surface the player is in contact with
    private bool contactingSurface = false;
    private float timeSinceContact; // Amount of time since the player was last in contact with something

    private float speed; // Squared speed
    private float xzSpeed; // Squared speed travelling in xz plane only
    private float inputFalloff; // How much current speed affects speed added through input
    private float breakFactor; // From 0 to 1, how closely our input corresponds to our current velocity

    private PostProcessing postProcessing;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        collisionListScript = GetComponent<CollisionList>();
        cam = GetComponentInChildren<Camera>();
        tr = transform;
        normalizedInput = new Vector3();
        jumpDirection = new Vector3();
        contactSurfaceNormal = new Vector3();
        postProcessing = GameObject.FindGameObjectWithTag("PostProcessing").GetComponent<PostProcessing>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSurfaceDirections();
        // Update members
        xzSpeed = rB.velocity.x * rB.velocity.x + rB.velocity.z * rB.velocity.z; // Find speed travelling in xz plane
        speed = xzSpeed + rB.velocity.y * rB.velocity.y;
        postProcessing.SpeedEffects(speed);

        ReadInput();

        if (vInput != 0 || hInput != 0) // Interpolate current velocity to new velocity in direction of input
        {
            Vector3 InputToWorld = tr.TransformDirection(normalizedInput); // Find the direction our input points to in world coordinates

            Vector3 desiredVelocity = new Vector3(InputToWorld.x, 0, InputToWorld.z) * -Mathf.Sqrt(xzSpeed); // Vector representing velocity if it were in the input direction
            desiredVelocity.y = rB.velocity.y; // Carry over old y velocity

            breakFactor = (Vector3.Dot(InputToWorld, rB.velocity.normalized) + 1) / 2;

            Vector3 newVelocity = Vector3.Lerp(rB.velocity, desiredVelocity, turnSpeed * Time.deltaTime); // Lerp between old velocity and desired input velocity

            if (breakFactor < breakThreashHold) // Change in direction is small enough that no speed is lost
            {
                newVelocity = newVelocity.normalized * Mathf.Sqrt(speed);
            }

            rB.velocity = newVelocity;
        }
    }

    void FixedUpdate()
    {
        inputFalloff = Mathf.Pow(1.2f, -xzSpeed / inputSpeedFalloff); // As player moves faster, input has less effect on speed
        rB.AddRelativeForce(hInput * bootAcceleration * inputFalloff * Time.deltaTime, 0, vInput * bootAcceleration * inputFalloff * Time.deltaTime); // Add keyboard input as a force

        if (jumpInput && collisionListScript.currentCollisionObjects.Count > 0) // If player attempted to jump and is in contact with a surface
        {
            jumpDirection = contactSurfaceNormal; // Jump direction is based on the normal of the surface in contact with
            jumpDirection = Vector3.Lerp(jumpDirection, Vector3.up, jumpUpwardsBias); // Lerp jump direction towards player's up
            rB.AddForce(jumpDirection * jumpPower); // Add force in the direction of the jump
            jumpInput = false;
        }

        if (timeSinceContact < 0.03 && rB.useGravity) // If in contact with a surface, player is able to wallrun based on the angle of the surface
        {
            float surfaceDotUp = 1 - Vector3.Dot(contactSurfaceNormal, Vector3.up);
            surfaceDotUp = Mathf.Clamp(surfaceDotUp, 0, 1); // Limits wallrunning ability on upside down surfaces
            rB.AddForce(Vector3.up * 9.81f * wallRunAbility * surfaceDotUp, ForceMode.Acceleration);
        }

        rB.transform.Rotate(Vector3.up * mouseX * Time.deltaTime); // Add mouse input directly
        cam.transform.Rotate(Vector3.right * mouseY * mouseInversion * Time.deltaTime); // Add vertical mouse movement

    }

    void ReadInput()
    {
        hInput = Input.GetAxisRaw("Horizontal"); // Effect of input decreases at higher speeds
        vInput = Input.GetAxisRaw("Vertical");

        normalizedInput.x = -hInput;
        normalizedInput.z = -vInput;
        normalizedInput.Normalize();

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (Input.GetKeyDown(KeyCode.Space)) // Spacebar pressed, register jump input
        {
            jumpInput = true;
            timeSinceJumpInput = 0;
        }

        if (jumpInput)
        {
            timeSinceJumpInput += Time.deltaTime; // track time since player tried to jump
            if (timeSinceJumpInput > 0.25f)
            {
                jumpInput = false;
            }

        }

        if (Input.GetMouseButtonDown(0)) // Left click on this frame
        {
            if (gear1 != null) // If there is gear assigned to slot 1
                gear1.Use(); // Use the gear in slot 1
        }
        if (Input.GetMouseButtonDown(1)) // Right click on this frame
        {
            if (gear2 != null) // If there is gear assigned to slot 1
                gear2.Use(); // Use the gear in slot 1
        }
    }

    // Check what surfaces the character is in contact with and update the surface normal
    void UpdateSurfaceDirections()
    {
        if (collisionListScript.currentCollisions.Count > 0) // If there were any contact points
        {
            contactingSurface = true;
            timeSinceContact = 0;
            contactSurfaceNormal.Set(0, 0, 0); // Reset surface normal

            // Angle of jump is based on all objects currently in contact with
            foreach (ContactPoint contactPoint in collisionListScript.currentContactPoints) // For each point that the player is in contact with something
            {
                contactSurfaceNormal += contactPoint.normal; // total up the normals of the contact points to find the average direction
            }
            contactSurfaceNormal = contactSurfaceNormal / collisionListScript.currentCollisions.Count; ; // Normalize direction
        }
        else
        {
            contactingSurface = false;
            timeSinceContact += Time.deltaTime;
        }
    }

    /* Parameter debug
    void OnGUI()
    {

        GUILayout.BeginArea(new Rect(20, 20, 160, 160));
        GUILayout.Label("Velocity: " + rB.velocity);
        GUILayout.Label("Speed: " + speed);
        //GUILayout.Label("ContactingSurface: " + contactingSurface);
        //GUILayout.Label("Input: " + new Vector3(hInput, 0, vInput));
        //GUILayout.Label("Normalized Input: " + normalizedInput);
        //GUILayout.Label("contactSurfaceNormal: " + contactSurfaceNormal);
        //GUILayout.Label("V dot input: " + breakFactor);
        //GUILayout.Label("Time since contact: " + timeSinceContact);
        GUILayout.EndArea();

    }*/

    public float GetSpeed()
    {
        return speed;
    }

    // simple boolean function checking if there is collision between player and specific objects
    public bool collideWith()
    {
        for (int i = 0; i < collisionListScript.currentCollisions.Count; i++)
        {
            if (collisionListScript.currentCollisions[i].gameObject.tag == "Ramp")
            {
                return true;
            }
        }
        return false;
    }

    public void EquipGear1(Gear newGear1)
    {
        gear1 = newGear1;
    }

    public void EquipGear2(Gear newGear2)
    {
        gear2 = newGear2;
    }

    public Vector3 GetContactSurfaceNormal()
    {
        return contactSurfaceNormal;
    }
}
