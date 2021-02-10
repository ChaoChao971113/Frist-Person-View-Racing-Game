using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionList : MonoBehaviour
{
    // Declare and initialize a new List of GameObjects called currentCollisions.
    public List<GameObject> currentCollisionObjects = new List<GameObject>();
    public List<Collision> currentCollisions;
    public List<ContactPoint> currentContactPoints;

    private List<Collision> collisionsThisFrame = new List<Collision>();
    private List<ContactPoint> contactsThisFrame = new List<ContactPoint>();


    void OnCollisionEnter(Collision col)
    {
        // Add the GameObject collided with to the list.
        currentCollisionObjects.Add(col.gameObject);
    }

    void OnCollisionExit(Collision col)
    {
        // Remove the GameObject collided with from the list.
        currentCollisionObjects.Remove(col.gameObject);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        collisionsThisFrame.Add(collisionInfo); // Add collision to list of collisions on current frame
        foreach(ContactPoint contact in collisionInfo.contacts ) // Add points of contact to list of all contact points on this frame
        {
            contactsThisFrame.Add(contact);
        }
    }

    void Start()
    {
        currentCollisions = new List<Collision>(); 
        currentContactPoints = new List<ContactPoint>(); 
    }


    void Update()
    {
        currentCollisions = new List<Collision>(collisionsThisFrame); // Copy over collisions from this frame
        currentContactPoints = new List<ContactPoint>(contactsThisFrame); // Copy over contact points from this frame
        collisionsThisFrame.Clear(); // Clear collisions from this frame
        contactsThisFrame.Clear();
    }
}

