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
    Rigidbody rb;

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

    public KeyCode jumpKey = KeyCode.Space;


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
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
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
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //movement direction
        moveDirection = playerOrientation.forward * vertInput + playerOrientation.right * horizInput; //walk in the direction you look
        //add force only when grounded
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl() //stop the player from getting too fast- remove this if we want to be able to gain speed as we move?
    {
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
}
