/*
 * Controls gameplay (pause, restart, etc.)
 * Holds win condition
 *
 * How to Delay a Function via Time:
 * http://answers.unity3d.com/questions/162148/delay-a-function-via-time.html
 *
 * Code for Pause Function:
 * http://answers.unity3d.com/questions/50246/how-to-make-a-pause-function.html
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Paint Buckets
    public GameObject paintBuckets; // Reference to empty GameObject with CreatePaintBuckets script attached

    // Room
    public GameObject roomObject; // Reference to Room prefab
    CreateRoom room; // Reference to Room's CreateRoom script

    Paintable[] paintables; // Reference to paintable objects in the scene
    int numOfPaintables;

    NonPaintable[] nonPaintables; // Reference to non-paintable objects in the scene
    int numOfNonPaintables;

    // Player
    public GameObject player; // Reference to empty game object that contains PlayerController

    // Enemies
    public GameObject enemies; // Reference to empty game object that contains EnemyManager
    EnemyManager enemyManager;

    // UI
    public GameObject endGamePanel;
    public GameObject colorPickerPanel;

    // Game Variables
    public bool roomColored;
    public bool startedColoring;

    float endTime = 0f; // Reference to the time when the room is finished being colored
    public float timeDelay = 3f; // Delay between end of game and the appearance of the end game menu
    
    bool paused; // Whether the game is paused for the end game menu
    bool gameFinished; // Whether the game has been completed

    void Awake()
    {
        roomColored = false;
        startedColoring = false;
        gameFinished = false;

        room = roomObject.GetComponent<CreateRoom>();
    }

    void Start()
    {
        paintables = room.paintableObjects;
        numOfPaintables = paintables.Length;

        nonPaintables = room.nonPaintableObjects;
        numOfNonPaintables = nonPaintables.Length;

        paused = false;
        //endGamePanel.SetActive(false);

        enemyManager = enemies.GetComponent<EnemyManager>();
    }

	void Update () {

        if (!paused && !gameFinished) {
            // If the player hasn't started coloring and uses one of the paint controls
            // Doesn't include the white paint
            if (!startedColoring && ((Input.GetKeyDown(KeyCode.Comma) || (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.Slash)))))
            {
                startedColoring = true;

                // Start spawning the enemies
                enemyManager.StartSpawning();
            }

            // If the player started coloring and the room isn't completely colored
            else if (startedColoring && !roomColored)
            {
                for (int i = 0; i < numOfPaintables; i++)
                {
                    if (!paintables[i].isColored)
                    {
                        return;
                    }
                }
                // At this point, all the paintable objects are colored

                // So, color the rest of the objects!
                for (int k = 0; k < numOfNonPaintables; k++)
                {
                    nonPaintables[k].color();
                }

                enemyManager.Reset(); // Kill all enemies and stop spawning new ones
                roomColored = true; // All the paintable and non-paintable objects are colored
                endTime = Time.time; // Record time of completion
            }

            // If all the objects are colored
            else if (roomColored)
            {
                // If enough time has passed since all the objects were colored
                if (Time.time > endTime + timeDelay)
                {
                    gameFinished = true;
                    Pause(); // Prompt user with end game menu
                    return;
                }
            }
        }
    }

    public void Pause()
    {
        paused = true;

        // Freeze time
        Time.timeScale = 0;

        // Show the end game menu
        endGamePanel.SetActive(true);

        // Hide the color picker panel
        colorPickerPanel.SetActive(false);

    }

    public void Unpause()
    {
        // Show the color picker panel
        colorPickerPanel.SetActive(true);

        // Start time again
        Time.timeScale = 1;

        paused = false;

        // Hide the end game menu
        endGamePanel.SetActive(false);
    }

    // Resets room
    // Input is true for new colors, false for primary/default colors
   public void Reset(bool useNewColors)
   {
        Unpause();

        roomColored = false;
        startedColoring = false;
        gameFinished = false;

        // Reset Audio Manager
        AudioManager.audioManager.Reset();

        // Reset Color Manager
        ColorManager.colorManager.Reset(useNewColors); // Needs to be called first so everything else can reference it

        // Reset Room
        room.Reset();

        // Reset Paintbuckets (set active, color)
        paintBuckets.GetComponent<CreatePaintBuckets>().Reset();

        // Reset Player (position, paintbrush)
        player.GetComponent<PlayerController>().Reset();

        // Reset Enemy
        //enemyManager.Reset();

        // Reset Color Picker
        colorPickerPanel.GetComponent<ColorPicker>().Reset();
    }
}
