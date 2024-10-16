using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTextDisplay : MonoBehaviour
{
    public Text introTxt;
    public Color color;
    public float fadeTime = 1f;
    bool hasMoved = false;
    bool hasCompletedIntro = false;
    public bool testingDisable = false;
    Coroutine currentCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if(testingDisable) return;
        introTxt.text = "WASD to move";
        StartCoroutine(FadeText(introTxt, 0, 1, fadeTime));
    }

    void OnTriggerEnter(Collider col){
        if(hasCompletedIntro || testingDisable) return;
        if(col.gameObject.name == "IntroTextTrigger"){
            introTxt.text = "Shanghai Lion \n presents";
            StartCoroutine(FadeText(introTxt, 0, 1, fadeTime));
        }else if(col.gameObject.name == "TitleTextTrigger"){
            introTxt.text = "Kai Haavik's \n Interactive Music Portfolio";
            //StartCoroutine(FadeText(introTxt, 0, 1, fadeTime));
            StartCoroutine(FadeText(introTxt, 0, 1, fadeTime, 0f, true, 10f));
            hasCompletedIntro = true;
        }
    }

    void OnTriggerExit(Collider col){
        if(hasCompletedIntro || testingDisable) return;
        if(col.gameObject.name == "IntroTextTrigger"){
            StartCoroutine(FadeText(introTxt, 1, 0, fadeTime));
        }
    }

    public void StartIntro(){
        if(testingDisable) return;
        StartCoroutine(FadeText(introTxt, 1, 0, fadeTime, 2f));
    }

    private IEnumerator FadeText(Text txt, float startAlpha, float endAlpha, float fadeTime, float delay = 0f, bool fadeBack = false, float fadeBackDelay = 0f){
        yield return new WaitForSeconds(delay);

        Color fadeColor = color;
        fadeColor.a = startAlpha;
        float t = 0;

        while(t < 1f){
            fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            txt.color = fadeColor;
            t += Time.deltaTime / fadeTime;
            yield return null;
        }

        fadeColor.a = endAlpha;
        txt.color = fadeColor;

        if(fadeBack){
            StartCoroutine(FadeText(txt, endAlpha, startAlpha, fadeTime, fadeBackDelay));
        }
    }

    /*private IEnumerator FadeText(Text txt, float startAlpha, float endAlpha, float fadeTime, float delay = 0f, bool fadeBack = false, float fadeBackDelay = 0f){
        yield return new WaitForSeconds(delay);
        print("here");
        Color fadeColor = color;
        fadeColor.a = startAlpha;

        //yield return new WaitForSeconds(fadeTime);

        float t = 0;

        while(t < 1f){
            fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            txt.color = fadeColor;
            t += Time.deltaTime / fadeTime;
            yield return null;
        }

        fadeColor.a = endAlpha;
        txt.color = fadeColor;

        if(fadeBack){
            StartCoroutine(FadeText(txt, startAlpha, endAlpha, fadeTime, fadeBackDelay));
        }
    }*/
}
