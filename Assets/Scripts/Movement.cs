using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;
    float horizontalMovement;
    Vector3 moveDirection;
    public float velocityDamping;
    public float jumpForce = 2f;
    public float jumpCooldown;
    public float groundedDst;
    public float groundDrag, airDrag;
    public float audioFadeTime = 1f;
    bool readyToJump = true;
    bool grounded = true;
    Rigidbody2D rb;
    public GameObject tutorialText, titleText, songDisplay;

    List<Transform> groundChecks;
    public IntroTextDisplay introTextDisplay;
    bool hasStartedIntro = false;
    Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        introTextDisplay = GetComponent<IntroTextDisplay>();
        groundChecks = new List<Transform>();
        foreach (Transform child in transform) {
            groundChecks.Add(child);
        }
    }

    public bool GetKeyLeft(){
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }

    public bool GetKeyRight(){
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    public bool GetKeyJump(){
        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
    }

    bool GetKeyDownJump(){
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W);
    }

    void GetInput(){
        horizontalMovement = 0;
        if(GetKeyRight()) horizontalMovement++;
        if(GetKeyLeft()) horizontalMovement--;

        if(GetKeyJump() && readyToJump && grounded){
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(!hasStartedIntro && (horizontalMovement != 0 || readyToJump == false)){
            introTextDisplay.StartIntro();
            hasStartedIntro = true;
        }
    }

    void Update()
    {

        GetInput();
        SpeedControl();
        StateControl();

        /*if(rb.velocity.magnitude != 0)
        {
            tutorialText.SetActive(false);
            titleText.SetActive(false);
        }*/
        foreach (Transform groundCheck in groundChecks) {
            grounded = Physics2D.Raycast(groundCheck.transform.position, -transform.up, groundedDst);
        }
        
        // print(grounded);
    }

    void FixedUpdate(){
        MovePlayer();
    }

    void MovePlayer(){
        moveDirection = transform.right * horizontalMovement;
        // print(moveDirection);

        /*if(OnSlope() && !exitingSlope){
            // print("on slope");

            moveDirection = GetSlopeMoveDir() * moveSpeed;
            rb.velocity = Vector3.MoveTowards(rb.velocity, moveDirection, Time.fixedDeltaTime / velocityDamping);

        }else{*/
            /*Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Vector3 targetFlatVel = Vector3.MoveTowards(flatVel, moveDirection * moveSpeed, Time.fixedDeltaTime / velocityDamping);
            rb.velocity = new Vector3(targetFlatVel.x, rb.velocity.y, targetFlatVel.z);*/
        rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Force);
        // print(rb.velocity);
        //}
        //rb.useGravity = !OnSlope();
    }

    void Jump(){
        // rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        // print("Jump");
    }

    public void ResetJump(){
        readyToJump = true;
        //exitingSlope = false;
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }

    void SpeedControl(){
        /*if(OnSlope() && !exitingSlope){
            if(rb.velocity.magnitude > moveSpeed){
                rb.velocity = rb.velocity.normalized * moveSpeed;
                UpdateSpeedText(rb.velocity.magnitude);
            }
            
        }else{*/

            Vector3 flatVel = new Vector2(rb.velocity.x, 0);

            if(rb.velocity.magnitude > maxSpeed){
                Vector3 limitedVel = rb.velocity.normalized * maxSpeed;
                //UpdateSpeedText(limitedVel.magnitude);
                rb.velocity = new Vector2(limitedVel.x, rb.velocity.y);
            }else{
                //UpdateSpeedText(flatVel.magnitude);
            }
        //}
        
    }

    void StateControl(){
        if(grounded){
            rb.drag = groundDrag;
        }else{
            rb.drag = airDrag;
        }
    }

    public void FlipPlayer(){
        StartCoroutine(RotateGraphic(0.3f));
    }

    private IEnumerator RotateGraphic(float animSpeed){
        float t = 0;
        float currentAngle = transform.eulerAngles.z;
        while(t < 1f){
            float newAngle = Mathf.SmoothStep(currentAngle, currentAngle + 180, t);
            transform.eulerAngles = new Vector3(0, 0, newAngle);
            t += Time.deltaTime / animSpeed;
            yield return null;
        }
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
