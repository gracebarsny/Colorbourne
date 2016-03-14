/*
 * Attached to enemy in EnemyManager
 *
 * Behavior that controls movement
 * Bounces between objects in the room
 *
 * Code and Physics from Official Unity Tutorial:
 * https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/creating-a-breakout-game
 *
 */

using UnityEngine;
using System.Collections;

public class BounceAround : MonoBehaviour {

    public float initialVelocity = 130f;

    Rigidbody rb;
    bool enemyInPlay;
    Enemy enemyManager;
    bool moving;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemyManager = GetComponent<Enemy>();
    }

    void Start()
    {
        moving = false;
    }

	void Update () {
        if (!moving) // Called once
        {
                rb.isKinematic = false;
                rb.AddForce(new Vector3(initialVelocity, 0, initialVelocity));
                moving = true;
        }
	}

    // When the enemy collides with something other than the player or another enemy
    void OnCollisionEnter(Collision collision)
    {
        string colliderTag = collision.collider.gameObject.tag;
        if (colliderTag != "Player" && colliderTag != "Enemy")
        {
            // Add another force
            rb.AddForce(new Vector3(initialVelocity/2, 0, -initialVelocity/2));
        }
    }
}
