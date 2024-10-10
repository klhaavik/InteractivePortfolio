using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    Slider volume, lowBand, midBand, highBand, lowPass, highPass, reverbLevel, reverbTime;
    public float defaultVolume = 0.9f;
    public float defaultLowBandVal = 0.5f;
    public float defaultMidBandVal = 0.45f;
    public float defaultHighBandVal = 0.6f;
    public float defaultLowPassVal = 1f;
    public float defaultHighPassVal = 0f;
    public float defaultReverbLevel = 0.1f;
    public float defaultReverbDecayTime = 0.1f;
    public AudioMixer am;
    // Start is called before the first frame update
    void Start()
    {
        lowBand = GameObject.Find("EQ_LowBand").GetComponent<Slider>();
        midBand = GameObject.Find("EQ_MidBand").GetComponent<Slider>();
        highBand = GameObject.Find("EQ_HighBand").GetComponent<Slider>();
        lowPass = GameObject.Find("LowPass").GetComponent<Slider>();
        highPass = GameObject.Find("HighPass").GetComponent<Slider>();
        volume = GameObject.Find("Volume").GetComponent<Slider>();
        reverbLevel = GameObject.Find("ReverbLevel").GetComponent<Slider>();
        reverbTime = GameObject.Find("ReverbDecayTime").GetComponent<Slider>();

        ResetToDefaultValues();
    }

    public void UpdateLowBand()
    {
        am.SetFloat("LowBandGain", 0.5f + lowBand.value);
    }

    public void UpdateMidBand()
    {
        am.SetFloat("MidBandGain", 0.5f + midBand.value);
    }

    public void UpdateHighBand()
    {
        am.SetFloat("HighBandGain", 0.5f + highBand.value);
    }

    public void UpdateLowPass()
    {
        am.SetFloat("LowPassFreq", 10f + lowPass.value * 21190f);
    }

    public void UpdateHighPass()
    {
        am.SetFloat("HighPassFreq", 10f + highPass.value * 21190f);
    }

    public void UpdateVolume()
    {
        am.SetFloat("MasterVol", -80f + volume.value * 80f);
        // print("here");
    }

    public void UpdateReverbLevel()
    {
        am.SetFloat("ReverbLevel", -10000f + reverbLevel.value * 12000f);
        am.SetFloat("ReflectionsLevel", -10000f + reverbLevel.value * 9500f);
    }

    public void UpdateReverbDecayTime()
    {
        am.SetFloat("ReverbDecayTime", 2.5f + reverbTime.value * 17.5f);
    }

    public void ResetToDefaultValues()
    {
        volume.value = defaultVolume;
        lowBand.value = defaultLowBandVal;
        midBand.value = defaultMidBandVal;
        highBand.value = defaultHighBandVal;
        lowPass.value = defaultLowPassVal;
        highPass.value = defaultHighPassVal;
        reverbLevel.value = defaultReverbLevel;
        reverbTime.value = defaultReverbDecayTime;

        UpdateVolume();
        UpdateLowBand();
        UpdateMidBand();
        UpdateHighBand();
        UpdateLowPass();
        UpdateHighPass();
        UpdateReverbLevel();
        UpdateReverbDecayTime();
    }
}
