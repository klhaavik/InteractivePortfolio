using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarWarsScroll : MonoBehaviour
{
    public float scrollSpeed = 5f;  // Speed of the text scrolling
    public float fadeDistance = 30f;  // Distance after which the text starts to fade
    public float endYPosition = 50f;  // Position at which the text stops

    private TextMeshProUGUI textMeshPro;
    private CanvasGroup canvasGroup;
    private float startPos;
    private float startingScale;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startPos = 0f + transform.position.y;
        startingScale = 0f + transform.localScale.x;
        //canvasGroup = gameObject.AddComponent<CanvasGroup>(); // For fading effect
    }

    void Update()
    {
        // Move text upwards
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        // Fade out text
        float distanceFromStart = transform.position.y - startPos;
        if (distanceFromStart > fadeDistance)
        {
            float fadeFactor = 1 - distanceFromStart;
            //textMeshPro.color = new Color(255,255,255,fadeFactor * 255);
        }

        // Scale down text as it moves up for perspective effect
        float scaleFactor = Mathf.Lerp(startingScale, startingScale / 1.25f, distanceFromStart / (endYPosition - startPos));
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        // Stop scrolling if it reaches the end position
        if (transform.position.y >= endYPosition)
        {
            Destroy(gameObject); // Optional: Destroy text after it scrolls out of view
        }
    }
}

