using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// projectile spawner
/// </summary>
public class ProjectileSpawner : MonoBehaviour
{

    public float FireSpeed = 2f;
    public GameObject ProjHazPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f, FireSpeed); //method, float time, float repeat
    }

    public void Shoot() //spawns the bullet
    {
        Instantiate(ProjHazPrefab, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
    }
}
