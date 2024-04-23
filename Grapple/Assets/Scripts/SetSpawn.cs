using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawn the player anywhere that isn't the abyss
/// </summary>
public class SetSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = gameObject.transform.position;
        //GetComponent<Health>().startPosition = gameObject.transform.position; //change start position to wherever this thing is
    }

}
