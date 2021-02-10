using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float timeToLive; // How long to rocket travels for before despawning
    public Rigidbody playerRB;
    public float explosionStrength;
    public float maxExplosionRange; // Distance at which the rocket will not affect the player

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToLive <= 0) // Rocket destroys itself after a specified amount of time
        {
            Explode();
        }
        else
        {
            timeToLive -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Explode(); // Rocket explodes on contact with anything
    }

    void Explode()
    {
        float distanceToPlayer = Vector3.Distance(playerRB.transform.position, transform.position);

        if(distanceToPlayer > 0 && distanceToPlayer < maxExplosionRange) // rocket will launch player
        {
            Vector3 directionToPlayer = (playerRB.transform.position - transform.position).normalized; // get a normalized direction to the player from the point of explosion

            float forceOfExplosion; // How much the rocket will affect the player; based on the distance from the player 
            forceOfExplosion = Remap(distanceToPlayer, 0, maxExplosionRange, 1, 0); // remap the distance to a value between 0 and 1
            Debug.Log(forceOfExplosion);
            playerRB.AddForce(directionToPlayer * explosionStrength * forceOfExplosion);
            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
