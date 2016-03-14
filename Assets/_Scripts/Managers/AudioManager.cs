/*
 * Contains game audio sources (but not all)
 * Static instance that all other scripts can refer to
 *
 */

using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager audioManager; // Static instance

    AudioSource[] allAudio; // Reference to all AudioSources attached to the AudioManager

    // Variables of AudioSources within allAudio
    AudioSource[] backgroundMusic;
    AudioSource pickUpBucketSound;
    AudioSource fadeColorSound;
    AudioSource enemyDiesSound;

    int currentMusicIndex = 0; // Index of background loop
    AudioSource nextSong; // Next background audio

    bool fadingOut = false;
    public float fadeSpeed;


    void Awake()
    {
        // Initialize Variables
        audioManager = this;
        allAudio = GetComponents<AudioSource>();

        backgroundMusic = new AudioSource[4] { allAudio[0], allAudio[1], allAudio[2], allAudio[3] };
        pickUpBucketSound = allAudio[4];
        fadeColorSound = allAudio[5];
        enemyDiesSound = allAudio[6];

        // Play the first background track on Awake
        backgroundMusic[0].Play(); 
    }

    // Start background music over from beginning
    public void Reset()
    {
        nextSong.Stop(); // Stop the previous song
        backgroundMusic[0].volume = 1; // Reset to 1 after being faded out to 0 in previous playthrough
        backgroundMusic[0].Play();
        currentMusicIndex = 0;
    }

    public void changeBackgroundMusic()
    {
        if (currentMusicIndex < 3)
        {
            // Fade out current music
            fadeOut(backgroundMusic[currentMusicIndex]);

            // Set the next song to be played
            nextSong = backgroundMusic[currentMusicIndex+1];
            nextSong.volume = 1; // Reset to 1 after being faded out to 0 in previous playthrough
            nextSong.Play();
        }
    }

    void fadeOut(AudioSource currentMusic)
    {
        if (currentMusic.volume > 0)
        {
            fadingOut = true;
            currentMusic.volume -= fadeSpeed * Time.deltaTime;
        }
        else
        {
            fadingOut = false;
            currentMusicIndex++;
        }
    }
	
	void Update () {
        if (fadingOut)
        {
            fadeOut(backgroundMusic[currentMusicIndex]);
        }
	}

    // Called when Player collides with a PaintBucket in PickUpBuckets
    public void pickUpBucket()
    {
        pickUpBucketSound.Play();
    }

    // Called when a Paintable or Non-Paintable object loses its color
    public void fadeColor()
    {
        fadeColorSound.Play();
    }

    // Called when an enemy dies in Enemy
    public void enemyDies()
    {
        enemyDiesSound.Play();
    }
}
