using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col){
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, -Physics2D.gravity.y);
    }
}
