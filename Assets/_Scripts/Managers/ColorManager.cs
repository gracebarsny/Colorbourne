/*
 * Contains static instance that all other scripts can refer to
 *
 * Holds game color variables
 * Sets future color palettes
 * Controls sound played with each color
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorManager : MonoBehaviour
{

    public static ColorManager colorManager; // Static instance

    ColorPalette[] palettes; // Reference to all palettes

    public Color[] colorArray; // Reference to current palette
    public Color[] nextColorArray; // Reference to next palette

    int lastColorArrayIndex = 0; // Reference to the index of the last palette used, initialized at 0 (default palette)

    AudioSource[] audioSources;

    void Awake()
    {
        if (colorManager == null)
        {
            colorManager = this;
            palettes = GetComponentsInChildren<ColorPalette>();
            audioSources = GetComponents<AudioSource>();

            Reset(false); // Reset using default colors

        }
        else
        {
            Destroy(this);
        }

    }

    public void Reset(bool useNewColors)
    {
        if (useNewColors)
        {
            colorArray = nextColorArray; // Old colorArray is replaced with the new colorArray
        }
        else
        {
            colorArray = palettes[0].colorPalette; // Use default colors (RGB)
            lastColorArrayIndex = 0;
        }
        nextColorArray = pickNextColorArray(lastColorArrayIndex); // Set the next colorArray 
    }

    // Picks nextColorArray that is different from the last one
    Color[] pickNextColorArray(int lastColorArrayIndex)
    {
        // Never includes the default palette
        int randomColorPaletteIndex = Random.Range(1, palettes.Length);

        // If the new randomColorPaletteIndex is the same as the last one
        while (randomColorPaletteIndex == lastColorArrayIndex)
        {
            // Re-randomize until it's a new one
            randomColorPaletteIndex = Random.Range(1, palettes.Length);
        }

        lastColorArrayIndex = randomColorPaletteIndex;
        return palettes[randomColorPaletteIndex].colorPalette;
    }

    public Color[] ColorArray
    {
        get { return colorArray; }
    }

    public Color[] NextColorArray
    {
        get { return nextColorArray; }
    }

    // Called when paint is applied on a RoomObject
    // Plays corresponding sound
    public void PlayAudioSource(Color paintColor)
    {
        // Finds paintColor in color array
        for (int i = 0; i < colorArray.Length - 1; i++)
        {
            if (paintColor == colorArray[i])
            {
                // Plays audio source associated with that color
                audioSources[i].Play();
                return;
            }
        }
    }
}
