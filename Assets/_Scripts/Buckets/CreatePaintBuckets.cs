/*
 * Attached to PaintBuckets (empty GameObject)
 *
 * Spawns paint buckets in the beginning of playthrough
 * Sets paint bucket colors according to the active palette in ColorManager
 */

using UnityEngine;
using System.Collections;

public class CreatePaintBuckets : MonoBehaviour
{

    public GameObject paintBucketPrefab;
    public Vector3 scale;
    public Vector3 location1;
    public Vector3 location2;
    public Vector3 location3;

    public GameObject paintSpotLight;
    public float lightHeight;
    public Vector3 lightRotation;

    GameObject[] paintBuckets;
    GameObject[] spotlights;
    Vector3[] spawnLocations;

    Renderer rend;

    void Start()
    {
        paintBuckets = new GameObject[3];
        spotlights = new GameObject[3];
        spawnLocations = new Vector3[3] { location1, location2, location3 };

        spawnBuckets();
    }

    // Called once, when the scene is first loaded
    void spawnBuckets() {

        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnLocation = spawnLocations[i];

            // Spawn lights over bucket locations
            Vector3 lightLocation = new Vector3(spawnLocation.x, lightHeight, spawnLocation.z);
            GameObject spotlight = (GameObject)Instantiate(paintSpotLight, lightLocation, Quaternion.Euler(lightRotation));
            spotlight.transform.parent = gameObject.transform;
            spotlights[i] = spotlight; // Add the spotlight to the spotlight array

            // Spawn buckets
            GameObject paintBucket = (GameObject)Instantiate(paintBucketPrefab, spawnLocation, Quaternion.identity);
            paintBucket.transform.localScale = scale;
            paintBucket.transform.parent = gameObject.transform;
            paintBuckets[i] = paintBucket; // Add the bucket to the bucket array

            paintBucket.gameObject.tag = "Paint Bucket";
            paintBucket.gameObject.layer = 10;

            Bucket bucket = paintBucket.AddComponent<Bucket>();
            bucket.AddLight(spotlights[i]);

            MeshCollider collider = paintBucket.AddComponent<MeshCollider>();
            collider.convex = true;
            collider.isTrigger = true;
        }

        Reset(); // Resets colors of Paint Buckets
    }

    // Called in the beginning of the scene & on Reset in GameManager
    public void Reset()
    {
        // Get colorArray from Color Manager
        Color[] colorArray = ColorManager.colorManager.ColorArray;

        for (int i = 0; i < paintBuckets.Length; i++)
        {
            GameObject currentBucket = paintBuckets[i];

            // Make paint buckets appear again
            currentBucket.SetActive(true);

            // Make the bucket's spotlight appear again
            spotlights[i].SetActive(true);
            
            // Change their color to match the new array in Color Manager
            rend = currentBucket.GetComponent<Renderer>();
            rend.materials[2].color = colorArray[i];
            currentBucket.GetComponent<Bucket>().color = rend.materials[2].color;
        }
    }

    // Called in PickUpBuckets when the player collides with a bucket
    // Turns off the spotlight over the selected bucket
    public void TurnOffSpotlight(GameObject bucket)
    {
        for (int i = 0; i < paintBuckets.Length; i++)
        {
            if (bucket == paintBuckets[i])
            {
                spotlights[i].SetActive(false);
                return;
            }
        }
    }
}
