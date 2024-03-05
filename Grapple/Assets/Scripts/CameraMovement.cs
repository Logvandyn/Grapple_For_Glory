using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allow the camera to follow the player
/// </summary>
public class CameraMovement : MonoBehaviour
{
    public Transform camPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = camPos.position;
    }
}
