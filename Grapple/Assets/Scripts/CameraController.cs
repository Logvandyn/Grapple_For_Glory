using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera rotation
/// </summary>
/// 
public class CameraController : MonoBehaviour
{

    public float Xsensitivity;
    public float Ysensitivity;

    public Transform playerOrientation;
    float camXrot;
    float camYrot;
    public GameObject menu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //ensure we can't click things or see the cursor
        Cursor.visible = false;

    }

    void Update()
    {
        if (menu.gameObject.GetComponent<PauseMenu>().MenuUp == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (menu.gameObject.GetComponent<PauseMenu>().MenuUp == false)
        {
            Cursor.lockState = CursorLockMode.Locked; //ensure we can't click things or see the cursor
            Cursor.visible = false;
        }
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * Xsensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * Ysensitivity;

        camYrot += mouseX;
        camXrot -= mouseY; //thanks for being weird Unity

        //lock rotation so you don't give yourself nausea
        camXrot = Mathf.Clamp(camXrot, -90f, 90f);

        //rotate camera and player 
        transform.rotation = Quaternion.Euler(camXrot, camYrot, 0); //camera
        playerOrientation.rotation = Quaternion.Euler(0, camYrot, 0); //only rotate player side to side
    }

    public void DoFov(float endValue) //change FOV for grapple, speed boost, wall run, etc
    {
        GetComponent<Camera>().fieldOfView = endValue;
    }
}
