using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSwing : MonoBehaviour
{
    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse0;

    [Header("References")]
    public LineRenderer lr;
    public Transform cam;
    public Transform gunTip;
    public Transform player;
    public LayerMask canGrapple;
    public PlayerMovement pmove;
    public CameraController camscript;


    [Header("Swinging")]
    private float maxSwingDistance = 25;
    private Vector3 swingPoint;
    private SpringJoint joint;
    public float grappleFOV;

    private Vector3 currentGrapplePosition;

    [Header("Air Controls")]
    public Transform orientation;
    public Rigidbody rb;
    public float horizontalThrust;
    public float forwardThrust;
    public float CableSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();

        if (joint != null) AirMovement(); //if joint 
    }

    void LateUpdate()
    {
        DrawRope();
    }

    private void StartSwing()
    {
        //deactivate grapple
        GetComponent<Grapple>().EndGrapple();
        pmove.ResetRestriction();

        pmove.swinging = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, canGrapple))
        {
            //FOV
            camscript.DoFov(grappleFOV);

            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            // the distance grapple will try to keep from the grapple point 
            joint.maxDistance = distanceFromPoint * 1f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //customize values as you like
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.enabled = true;
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    public void StopSwing() //gee i sure wish i could use this, if only it were public
    {
        pmove.swinging = false;
        lr.positionCount = 0;
        Destroy(joint);
        lr.enabled = false;
        //reset FOV
        camscript.DoFov(90f);
        pmove.rb.mass = 5;
    }

    void DrawRope()
    {
        //if not grappling dont draw rooe
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }

    private void AirMovement()
    {
        //add force
        //right
        if (Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * horizontalThrust * Time.deltaTime);
        //left
        if (Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * horizontalThrust * Time.deltaTime);
        //forward
        if (Input.GetKey(KeyCode.W)) rb.AddForce(orientation.forward * forwardThrust * Time.deltaTime);
        //shorten
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrust * Time.deltaTime);
            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
        //extend
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float extendDistancePoint = Vector3.Distance(transform.position, swingPoint) + CableSpeed;

            joint.maxDistance = extendDistancePoint * 0.8f;
            joint.minDistance = extendDistancePoint * 0.25f;
        }
        
        
    }
}
