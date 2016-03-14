/*
 * Attached to objects that the player can paint
 *
 * Contains paint method
 * Fades out when hit by an enemy
 */

using UnityEngine;
using System.Collections;

public class Paintable : RoomObject
{
    public Color startingColor;

    void Start()
    {
        int randomColor = Random.Range(0, 3);
        startingColor = ColorManager.colorManager.ColorArray[randomColor];
        rend.material.color = startingColor;
        colorChanged = true;
        colorInc = 0.025f;
        Reset();
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
                else // Either shift key
                {
                    colorIndex = 3;
                }
                paint(colorArray[colorIndex]);
            }
        }
    }

    public void paint(Color paintColor)
    {
        // Plays sound corresponding to the paintColor
        // Called before mixing happens
        if (paintColor != colorArray[3]) // if paintColor is not the eraser color
        {
            isColored = true;
            colorManager.PlayAudioSource(paintColor);
        }

        // Clear color (Erase)
        else
        {
            isColored = false;
        }

        // Color Mixing
        if (isColored)
        {
            // Get current color
            currentColor = rend.material.color;

            // Find average of two colors
            paintColor = (paintColor + currentColor) / 2;
        }

        // Set new color
        rend.material.SetColor("_Color", paintColor);
        colorChanged = true;

    }

    void OnCollisionEnter(Collision col)
    {
        // If hit by an enemy
        if (col.collider.gameObject.tag == "Enemy")
        {
            Enemy enemy = col.collider.GetComponent<Enemy>();

            // Check the enemy's color
            Color enemyColor = enemy.getEnemyColor();

            // If the enemy is colored
            if (enemyColor != Color.black)
            {
                // Change color to enemy's color
                paint(enemyColor);
            }
            else
            {
                // Fade color
                colorInc = 0.05f;
                fadeOut();
            }
        }
    }
}
