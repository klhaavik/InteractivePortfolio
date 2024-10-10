using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSmoothCycle : MonoBehaviour
{
    float secondsToCycle;
    public float minSecondsToCycle, maxSecondsToCycle;
    SpriteRenderer sr;
    Color startingColor;
    float currentHue;
    public float saturation = 0.2f;
    float value = 1f;

    private void Start()
    {
        secondsToCycle = Random.Range(minSecondsToCycle, maxSecondsToCycle);
        sr = GetComponent<SpriteRenderer>();
        float startingHue = Random.value;
        startingColor = Color.HSVToRGB(startingHue, saturation, 1f);
        sr.color = startingColor;
        currentHue = startingHue;
    }

    void Update()
    {
        currentHue = (currentHue + Time.deltaTime / secondsToCycle) % 1;
        sr.color = Color.HSVToRGB(currentHue, saturation, 1f);
    }
}
