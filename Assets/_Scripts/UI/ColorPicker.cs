/*
 * Attached to colorPickerPanel (UI)
 *
 * Displays colors from ColorManager
 * Adds a new color when a paint bucket is picked up
 */


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorPicker : MonoBehaviour {
 
    int numOfColors;
    Image[] imgArray; // References to the 2D placeholders for the color on the color picker
    Color[] colorArray; // Reference to colorArray in ColorManager

    void Awake() {
        imgArray = GetComponentsInChildren<Image>();
        numOfColors = imgArray.Length - 1;
    }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        // Get updated color array from Color Manager
        colorArray = ColorManager.colorManager.ColorArray;

        // Set up the buttons
        buttonSetup();
    }

    void buttonSetup() { 
        // Skip first image in the array because that's the control UI (key icons, etc.)
        for (int i = 1; i < numOfColors; i++)
        {
            imgArray[i].color = colorArray[i - 1];
            imgArray[i].enabled = false; // at start, no buckets picked up so no colors are shown
        }
	}

    // Add color to the color picker
    // Called when a paint bucket is collected by the player in PickUpBuckets
    public void AddColor(Color c)
    {
        for (int i = 0; i < numOfColors; i++)
        {
            if (c == imgArray[i].color) // if the color added equals the color of the invisible color holder
            {
                imgArray[i].enabled = true; // show that color on the color picker
            }
        }
    }
}
