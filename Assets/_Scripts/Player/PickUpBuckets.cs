/*
 * Attached to Player in Player Controller
 *
 * Ability to pick up paint buckets
 * Adds the paint color to the color picker
 *
 */

using UnityEngine;
using System.Collections;

public class PickUpBuckets : MonoBehaviour {

    public ColorPicker colorPicker;

    // When the player collides with a Paint Bucket
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Paint Bucket")
        {
            // Get Bucket script
            Bucket bucket = other.GetComponent<Bucket>();

            // Add the new color to the Color Picker
            colorPicker.AddColor(bucket.color);

            // Deactivate the bucket and its spotlight
            bucket.spotlight.SetActive(false);
            other.gameObject.SetActive(false);

            // Play sound effects
            AudioManager.audioManager.changeBackgroundMusic();
            AudioManager.audioManager.pickUpBucket();
        }
    }
}
