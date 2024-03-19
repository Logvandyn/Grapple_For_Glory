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

    [Header("Grappling")]
    public float maxGrappleDist;
    public float grappleDelay;
    private Vector3 grapplePoint;
    public float cooldown;
    public float cooldownTimer;

    public KeyCode grappleKey = KeyCode.Mouse1;
    private bool grappling;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
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

    }

    private void EndGrapple() //end, cooldown
    {
        grappling = false;
        cooldownTimer = cooldown;

        lr.enabled = false;
    }
}
