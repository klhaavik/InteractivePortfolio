using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
    public string displayTxt;
    private StateManager sm;
    private AudioSource song;
    private float currentVol;
    private Coroutine stopSongCoroutine, fadeVolumeCoroutine;

    void Start()
    {
        song = GetComponent<AudioSource>();
        currentVol = song.volume;

        sm = GameObject.Find("Canvas").GetComponent<StateManager>();
    }

    public void PlaySong(float fadeTime = 0f)
    {
        if(fadeVolumeCoroutine != null) StopCoroutine(fadeVolumeCoroutine);
        if(stopSongCoroutine != null) StopCoroutine(stopSongCoroutine);
        song.Play();
        song.volume = 1f;
        sm.CurrentAudio = this;
    }

    public void StopSong(float fadeTime = 0f)
    {
        stopSongCoroutine = StartCoroutine(StopAndUpdateSong(fadeTime));
    }

    /*public void PlayAndUpdateSong(float fadeTime)
    {
        song.Play();

        if(fadeVolumeCoroutine != null) StopCoroutine(fadeVolumeCoroutine);
        currentVol = song.volume;
        fadeVolumeCoroutine = StartCoroutine(FadeVolume(currentVol, 1, fadeTime));

        sm.CurrentAudio = this;
    }*/

    public IEnumerator StopAndUpdateSong(float fadeTime)
    {
        if(fadeVolumeCoroutine != null) StopCoroutine(fadeVolumeCoroutine);
        currentVol = song.volume;
        fadeVolumeCoroutine = StartCoroutine(FadeVolume(currentVol, 0, fadeTime));
        yield return new WaitForSeconds(fadeTime);

        song.Stop();
    }

    IEnumerator FadeVolume(float startVal, float endVal, float fadeTime)
    {
        float t = 0;
        song.volume = startVal;

        while(t < 1f)
        {
            song.volume = Mathf.SmoothStep(startVal, endVal, t);
            t += Time.deltaTime / fadeTime;
            yield return null;
        }
        
        song.volume = endVal;
    }
}
