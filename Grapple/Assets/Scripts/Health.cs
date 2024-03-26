using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxhealth = 3;
    public int currenthealth;
    public int amount;
    public TextMeshProUGUI healthcounter;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        SetCountText();
    }

    public void TakeDamage(int amount)
    {
        currenthealth -= amount;

        if (currenthealth <= 0)
        {
            //Dead

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hazard")
        {
            TakeDamage(1);
        }
    }
 
   
        // Update is called once per frame
        void Update()
    {
        SetCountText();
    }

    public void SetCountText()
    {
        //counts the Health you have left
        healthcounter.text = "Health " + currenthealth.ToString();
    }
}
