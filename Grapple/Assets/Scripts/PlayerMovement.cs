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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //link rigidbody
        rb.freezeRotation = true; //so the player doesnt fall over
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
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
