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
    }

    private void MovePlayer()
    {
        //movement direction
        moveDirection = playerOrientation.forward * vertInput + playerOrientation.right * horizInput; //walk in the direction you look
        //add force
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
