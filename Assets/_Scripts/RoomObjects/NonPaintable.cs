/*
 * Attached to objects in Room that the Player can't paint directly, but will eventually turn color on completion of the game
 *
 * Contains method to determine eventual color based on the next palette in ColorManager
 * Has its own reset function
 */

using UnityEngine;
using System.Collections;

public class NonPaintable : RoomObject
{
    public bool timeToColor;
   
    void Start()
    {
        Reset();
    }

    public override void Reset()
    {
        colorManager = ColorManager.colorManager;
        colorArray = colorManager.NextColorArray;

        if (isColored)
        {
            fadeOut();
        }

        setEventualColor();
    }

    public void color()
    {
        timeToColor = true;
        tColor = 0;
        isColored = true;
        fadeIn(eventualColor);
    }

    void setEventualColor()
    {
        int i = Random.Range(0, colorArray.Length - 1); // Doesn't include eraser color
        eventualColor = colorArray[i];
    }
}

