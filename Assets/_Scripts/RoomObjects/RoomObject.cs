/*
 * Parent class for Paintable & Non-Paintable
 *
 * Contains functions for fading colors in and out
 */

using UnityEngine;
using System.Collections;

public class RoomObject : MonoBehaviour
{

    protected Renderer rend;
    protected Color currentColor;
    protected Color defaultColor;
    public bool isColored; // painted and not white
    protected bool colorChanged; // painted any color
    protected float tColor = 2f;
    protected float colorInc; // color increment used on tColor
    bool fadingOut = false;
    bool fadingIn = false;
    protected Color[] colorArray;
    protected ColorManager colorManager; // references static instance
    protected Color eventualColor;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }


    public virtual void Reset()
    {
        colorManager = ColorManager.colorManager;
        colorArray = colorManager.ColorArray;
        fadeOut();
    }

    void Update()
    {
        if (fadingOut)
        {
            if (tColor <= 1)
            {
                tColor += 0.025f;
                Color newColor = Color.Lerp(currentColor, defaultColor, tColor);
                rend.material.SetColor("_Color", newColor);
            }
            else
            {
                tColor = 2f;
                fadingOut = false;
            }
        }

        // Only Non-Paintable object fades in
        else if (fadingIn)
        {
            if (tColor <= 3.5)
            {
                tColor += 0.05f;
                Color newColor = Color.Lerp(defaultColor, eventualColor, tColor);
                rend.material.SetColor("_Color", newColor);
            }
            if (rend.material.color == eventualColor)
            {
                currentColor = eventualColor;
                isColored = true;
                fadingIn = false;
            }
        }
    }

    // Called in Reset function & when a Paintable Object is hit by the enemy
    public void fadeOut()
    {
        currentColor = rend.material.color;

        // If paint was applied (including white)
        if (colorChanged || isColored)
        {
            //rend.material.SetColor("_Color", defaultColor);

            // Color is removed
            isColored = false;
            colorChanged = false;

            // Trigger fadingOut in Update function
            tColor = 0;
            fadingOut = true;

            // Play fading out sound effect
            AudioManager.audioManager.fadeColor();
        }

        // Instant change, no fading out
        Color newColor = currentColor - (Color.white / 8);
        rend.material.SetColor("_Color", newColor);
    }

    // Called in Non-Paintable Object at the end of the game
    public void fadeIn(Color c)
    {
        eventualColor = c;
        fadingIn = true;
    }
}
