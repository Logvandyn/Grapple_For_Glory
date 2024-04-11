using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player movement on X and Z
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform playerOrientation;
    Vector3 moveDirection;
    public Rigidbody rb;

    //keyboard
    float horizInput;
    float vertInput;

    //drag to stop slipping- TAKE THIS OUT IF WE WANT SLIPPERY MOVEMENT
    //make sure we're on the ground to only apply drag when grounded
    public float groundDrag;
    public float playerHeight;
    public LayerMask whatIsGround; //get what is set as the ground
    bool grounded;

    //jumping
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true; //setting it to true fixes the issue of, well, not working
    public int jumpCount;

    public KeyCode jumpKey = KeyCode.Space;

    //freezing during grapple shoot- TAKE THIS AWAY IF YOU WANT TO MOVE WHILE THE GUN SHOOTS
    public bool freeze;

    public bool activeGrapple;
    private Vector3 velToSet;
    public bool enableMoveOnNextTouch; //move once you land

    //swing movement
    public float swingSpeed;
    public bool swinging;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //link rigidbody
        rb.freezeRotation = true; //so the player doesnt fall over
    }

    // Update is called once per frame
    void Update()
    {

        //drag stuff- I CAST RAYCAST
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        MyInput();
        SpeedControl();

        //apply drag
        if (grounded && !activeGrapple)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //remove this to move during shoot
        if (freeze)
        {
            rb.velocity = Vector3.zero;
        }

        if (swinging)
        {
            moveSpeed = swingSpeed;
            rb.mass = 1;
        }

        if (GetComponent<Grapple>().grappleFall == true)
        {
            rb.mass = 5;
        }
        if (GetComponent<Grapple>().grappleFall == false)
        {
            rb.mass = 1;
        }
    }

    private void FixedUpdate() //fixed for physics
    {
        MovePlayer(); 
    }

    //keyboard inputs
    private void MyInput()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded) //if space, ready, and on ground
        {
            readyToJump = false;
            //jumpCount = jumpCount + 1;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        /*if (jumpCount == 2)
        {
            readyToJump = false;
        }*/
    }

    public void ResetRestriction() //let you move again
    {
        activeGrapple = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMoveOnNextTouch)
        {
            enableMoveOnNextTouch = false;
            ResetRestriction();

            GetComponent<Grapple>().EndGrapple(); //make sure it's public you doofus
        }
        if (collision.gameObject.tag == "Hazard")
        {
            GetComponent<GrappleSwing>().lr.enabled = false;
            GetComponent<GrappleSwing>().StopSwing();
        }
    }

    private void MovePlayer()
    {
        //deactivate while grappling and swinging
        if (activeGrapple) return;
        if (swinging) return;

        //movement direction
        moveDirection = playerOrientation.forward * vertInput + playerOrientation.right * horizInput; //walk in the direction you look
        //add force only when grounded
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            //rb.mass = 1;
            GetComponent<Grapple>().grappleFall = false;
            jumpCount = 0;
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            //rb.mass = 5;
        }
    }

    private void SpeedControl() //stop the player from getting too fast- remove this if we want to be able to gain speed as we move?
    {
        //deactivate while grappling
        if (activeGrapple) return;

        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //limit velocity if needed
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); //apply max velocity
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    //grapple stuff
    public Vector3 CalcJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    public void jumpToPos(Vector3 targetPos, float trajectoryHeight)
    {
        activeGrapple = true;
        velToSet = CalcJumpVelocity(transform.position, targetPos, trajectoryHeight);
        //rb.velocity = CalcJumpVelocity(transform.position, targetPos, trajectoryHeight);
        //delay
        Invoke(nameof(SetVel), 0.1f); //apply velocity after 0.1 seconds

        //stop grappling after X time in case you get stuck like Ryan in Substance Painter
        Invoke(nameof(ResetRestriction), 3f); //change this number if needed
    }

    private void SetVel() //apply force 
    {
        enableMoveOnNextTouch = true;
        rb.velocity = velToSet;
    }
}
