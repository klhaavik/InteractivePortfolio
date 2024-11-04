using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class TextScroll : MonoBehaviour
{
    public float scrollSpeed = 5f;  // Speed of the text scrolling
    public float fadeDistance = 30f;  // Distance after which the text starts to fade
    public float endYPosition = 50f;  // Position at which the text stops

    private TextMeshProUGUI textMeshPro;
    private CanvasGroup canvasGroup;
    private Vector3 startPos;
    private float startingScale;
    private Dictionary<string, string> songBlurbs;
    bool started = false;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        startingScale = 0f + transform.localScale.x;

        songBlurbs = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, "songBlurbs.txt");

        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);

            print(content);

            string[] paragraphs = content.Split(new string[] { "\n\n\n\n", "\r\n\r\n\r\n\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i < paragraphs.Length; i+=2){
                songBlurbs[paragraphs[i]] = paragraphs[i+1];
            }
        }
        else
        {
            Debug.LogError("File not found at path: " + filePath);
        }
    }

    void Update()
    {
        if(!started) return;
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        float distanceFromStart = transform.position.y - startPos.y;
        if (distanceFromStart > fadeDistance)
        {
            float fadeFactor = 1 - distanceFromStart;
        }

        /*float scaleFactor = Mathf.Lerp(startingScale, startingScale / 1.25f, distanceFromStart / (endYPosition - startPos.y));
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);*/

        if (transform.position.y >= endYPosition)
        {
            started = false;
        }
    }

    public void StartTextScroll(string title){
        started = true;
        textMeshPro.text = songBlurbs[title];
    }

    public void StopTextScroll(){
        started = false;
        transform.localScale = new Vector3(startingScale, startingScale, startingScale);
        transform.position = startPos;
    }
}

