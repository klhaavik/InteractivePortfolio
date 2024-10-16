using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSmoothCycle : MonoBehaviour
{
    float secondsToCycle;
    public float minSecondsToCycle, maxSecondsToCycle;
    float offsetMagnitude;
    public float minOffset, maxOffset;
    SpriteRenderer sr;
    Color startingColor;
    float currentHue;
    public float saturation = 0.2f;
    float value = 1f;
    float zOffset;
    float zPos;
    float theta = 0;

    private void Start()
    {
        secondsToCycle = Random.Range(minSecondsToCycle, maxSecondsToCycle);
        sr = GetComponent<SpriteRenderer>();
        float startingHue = Random.value;
        startingColor = Color.HSVToRGB(startingHue, saturation, 1f);
        sr.color = startingColor;
        currentHue = startingHue;
        offsetMagnitude = Random.Range(minOffset, maxOffset);
        zPos = transform.position.z;
    }

    void Update()
    {
        currentHue = (currentHue + Time.deltaTime / secondsToCycle) % 1;
        sr.color = Color.HSVToRGB(currentHue, saturation, 1f);
        zOffset = Mathf.Sin(theta) * offsetMagnitude;
        theta += 2 * Mathf.PI / secondsToCycle * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos + zOffset);
    }
}
