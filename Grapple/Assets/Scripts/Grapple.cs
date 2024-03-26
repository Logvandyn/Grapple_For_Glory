using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Grapple and pull towards objects
/// </summary>

public class Grapple : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement movement;
    public Transform cam;
    public Transform gunTip;
    public LayerMask canGrapple;
    public LineRenderer lr;
    public CameraController camscript;
    


    [Header("Grappling")]
    public float maxGrappleDist;
    public float grappleDelay;
    private Vector3 grapplePoint;
    public float cooldown;
    private float cooldownTimer;
    public float overshootY;
    public float grappleFOV;

    public KeyCode grappleKey = KeyCode.Mouse1;
    private bool grappling;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        lr.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();
        //count down cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        //update line position
        if (grappling)
        {
            lr.SetPosition(0, gunTip.position);
        }
    }

    private void StartGrapple() //shoot
    {
        if (cooldownTimer > 0) return; //if cooldown is active dont grapple
        grappling = true;

        //remove this to move during shoot
        //movement.freeze = true;

        //raycast to shoot
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDist, canGrapple))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelay); //if hit, do this
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDist;
            Invoke(nameof(EndGrapple), grappleDelay); //if miss, end
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple() //pull
    {
        //remove this to move during shoot
        //movement.freeze = false;

        //FOV
        camscript.DoFov(grappleFOV);

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeY = grapplePoint.y - lowestPoint.y;
        float highestPointArc = grapplePointRelativeY + overshootY;

        if (grapplePointRelativeY < 0) highestPointArc = overshootY;

        movement.jumpToPos(grapplePoint, highestPointArc);

        Invoke(nameof(EndGrapple), 1f);
    }

    public void EndGrapple() //end, cooldown
    {
        //reset FOV
        camscript.DoFov(90f);
        //remove this to move during shoot
        //movement.freeze = false;

        grappling = false;
        cooldownTimer = cooldown;

        lr.enabled = false;
    }
}
