using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public AudioManager am;
    public Movement movement;
    private AudioSourcePlayer currentAudio;
    public AudioSourcePlayer CurrentAudio { get; set; }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        movement.ResetToStart();
        print("resetting");

        am.ResetToDefaultValues();
        CurrentAudio?.StopSong(movement.audioFadeTime);
    }
}
