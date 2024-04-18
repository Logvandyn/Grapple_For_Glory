using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour
{
    public GameObject leftpoint;
    public GameObject rightpoint;
    private Vector3 leftpos;
    private Vector3 rightpos;
    public int speed;
    public bool goingLeft;

    // Start is called before the first frame update
    void Start()
    {// Left position grabs the trasform from the leftPoint GameObject 
        leftpos = leftpoint.transform.position;
        // same goes for right
        rightpos = rightpoint.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    //this fuction will allow our enemy to move
    private void movement()
    {
        if (goingLeft == true)
        {
            if (transform.position.x <= leftpos.x)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.x >= rightpos.x)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
    }
}
