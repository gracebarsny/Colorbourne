/*
 * Attached to Enemies (empty GameObject)
 *
 * Spawns/Instantiates enemies (5 max at a time)
 * Adds movement script to each enemy
 * Controls initial velocity
 * Contains reset function which kills all enemies in the room
 */

using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyPrefab; // Reference to enemy prefab
    public Transform[] spawnPoints; // References to where the enemies will spawn
    public float spawnTime = 5f; // Time the first enemy spawns
    public float startingVelocity;
    float velocity; // Reference to current velocity
    public float velocityIncrease;
    public float maxVelocity;

    Enemy[] enemies;

    bool spawning;

    AudioSource spawnSound;

    public int numOfEnemies = 0; // Number of enemies in the scene, accessed by Enemy script

    public static EnemyManager enemyManager;
   
    void Start()
    {
        enemyManager = this;
        spawnSound = GetComponent<AudioSource>();
    }

    public void Reset()
    {
        // Stop spawning enemies
        CancelInvoke("Spawn");

        // Kill all enemies
        enemies = GetComponentsInChildren<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Die();
        }
    }

    // Called in GameManager after the player paints the first object
	public void StartSpawning () {
        velocity = startingVelocity;
        InvokeRepeating("Spawn", spawnTime, 10f);
	}
	
    void Spawn()
    {
        // If there are less than 5 enemies in the room
        if (numOfEnemies < 5)
        {
            // Select a random spawnPoint
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnPointIndex];

            // Instantiate the enemy
            GameObject enemy = (GameObject) Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemy.transform.localScale = spawnPoint.localScale;

            // Give the enemy these properties in the hierarchy
            enemy.transform.parent = gameObject.transform; // Make each enemy the child of this object
            enemy.gameObject.tag = "Enemy"; // Give each enemy the tag "Enemy"

            // Play the spawn sound
            spawnSound.Play();

            // Add 1 to the number of enemies in the room
            numOfEnemies++;

            // Enemy Movement
            enemy.AddComponent<BounceAround>().initialVelocity = velocity;

            // For the next enemy
            velocity += velocityIncrease; // Each consecutive enemy is faster than the last one
            // When the velocity reaches the max velocity
            if (velocity > maxVelocity)
            {
                // Reset to the slowest velocity
                velocity = startingVelocity;
            }
        }
    }
}
