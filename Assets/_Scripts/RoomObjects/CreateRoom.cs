/*
 * Attached to Room prefab
 *
 * Contains references to all Paintable and Non-Paintable objects
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateRoom : MonoBehaviour
{
    public NonPaintable[] nonPaintableObjects; // Reference to all objects in the room that are Non-Paintable
    public Paintable[] paintableObjects; // Reference to all objects in the room that are Paintable

    void Awake()
    {
        nonPaintableObjects = GetComponentsInChildren<NonPaintable>();
        paintableObjects = GetComponentsInChildren<Paintable>();
    }

    // Called in GameManager
    public void Reset()
    {
        for (int i = 0; i < nonPaintableObjects.Length; i++)
        {
            nonPaintableObjects[i].Reset();
        }

        for (int j = 0; j < paintableObjects.Length; j++)
        {
            paintableObjects[j].Reset();
        }
    }
}
