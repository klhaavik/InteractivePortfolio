using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchy : MonoBehaviour
{
    public Movement player;
    void OnTriggerEnter2D(Collider2D col){
        print("here");
        Physics2D.gravity = -Physics2D.gravity;
        player.FlipPlayer();
    }
}
