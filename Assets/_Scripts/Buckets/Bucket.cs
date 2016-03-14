/*
 * Attached to Bucket prefab in CreatePaintBuckets
 *
 * Holds references to its color and the spotlight above it
 *
 */

using UnityEngine;
using System.Collections;

public class Bucket : MonoBehaviour {

    public GameObject spotlight;
    public Color color;

    public void AddLight(GameObject light)
    {
        spotlight = light;
    }
}
