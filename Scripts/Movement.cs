using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float audioFadeTime = 1f;
    Rigidbody2D rb;
    public GameObject tutorialText, titleText, songDisplay;
    Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * moveSpeed;

        if(rb.velocity.magnitude != 0)
        {
            tutorialText.SetActive(false);
            titleText.SetActive(false);
        }
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("MusicPiece"))
        {
            col.gameObject.GetComponent<AudioSourcePlayer>().PlaySong(audioFadeTime);
            songDisplay.SetActive(true);
            songDisplay.GetComponent<Text>().text = col.gameObject.GetComponent<AudioSourcePlayer>().displayTxt;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("MusicPiece"))
        {
            col.gameObject.GetComponent<AudioSourcePlayer>().StopSong(audioFadeTime);
            songDisplay.SetActive(false);
        }
    }
}
