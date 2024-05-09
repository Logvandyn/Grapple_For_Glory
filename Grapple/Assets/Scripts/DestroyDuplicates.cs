using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDuplicates : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject != player)
        {
            Destroy(this.gameObject);
        }
    }
}
