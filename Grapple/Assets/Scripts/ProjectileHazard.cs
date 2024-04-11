using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// flying hazard
/// </summary>
public class ProjectileHazard : MonoBehaviour
{
    public int speed;
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
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
