using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounceForce = 20f;
    void OnCollisionEnter2D(Collision2D col){
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(col.gameObject.transform.up * bounceForce, ForceMode2D.Impulse);
    }
}
