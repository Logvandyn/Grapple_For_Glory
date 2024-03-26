using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhealth = 3;
    public int currenthealth;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
    }

    public void TakeDamage(int amount)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
