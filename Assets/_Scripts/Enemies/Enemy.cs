/*
 * Attached to Enemy prefab
 *
 * Contains Die and Paint functions
 */

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    // Variables for Enemy Components
    Renderer rend;
    MeshCollider meshCollider;
    Rigidbody rb;
    public bool isColored;

	void Start () {
        rend = GetComponent<Renderer>();
        meshCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.collider.gameObject.tag == "Player")
        {

            if (((Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.Period)) || (Input.GetKeyDown(KeyCode.Slash)) || (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))))
            {
                int colorIndex;
                if (Input.GetKeyDown(KeyCode.Comma))
                {
                    colorIndex = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Period))
                {
                    colorIndex = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Slash))
                {
                    colorIndex = 2;
                }
                else
                {
                    colorIndex = 3;
                }
                paint(ColorManager.colorManager.ColorArray[colorIndex]);
            }
        }
    }

    public void paint(Color c)
    {
        rend.material.color = c;
        isColored = true;

        if (c == Color.white)
        {
            Die ();
        }
    }

    public Color getEnemyColor()
    {
        return rend.material.color;
    }

    public void Die()
    {
        // If it's not painted
        if (!isColored)
        {
            // Set its color to white
            // Needed when Die() is called directly from EnemyManager without going through paint function first
            rend.material.color = Color.white;
        }
        
        // Play sound effect when enemy dies
        AudioManager.audioManager.enemyDies();

        // Stop moving
        rb.velocity = Vector3.zero;

        Sink();
    }

    void Sink()
    {
        rb.constraints = RigidbodyConstraints.None; // Falls through the floor
        Destroy(gameObject, 2f); // Destroy enemy object after 2 seconds of falling
        EnemyManager.enemyManager.numOfEnemies--; // Reduce the number of enemies by 1
    }

}
