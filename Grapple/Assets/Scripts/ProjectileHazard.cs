using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// flying hazard
/// </summary>
public class ProjectileHazard : MonoBehaviour
{
    public int speed;
    public Rigidbody rb;
    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.forward * Time.deltaTime * speed;
        rb.AddForce(rb.transform.forward * Time.deltaTime * (speed * 10));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Scenery") //destroy the hazard if it hits things
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
