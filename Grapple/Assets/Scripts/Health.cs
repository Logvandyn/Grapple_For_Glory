using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxhealth = 3;
    public int currenthealth;
    public int maxlives = 3;
    public int currentlives;
    public int amount;
    private Vector3 startPosition; //for spawning / respawning
    public TextMeshProUGUI healthcounter;
    public TextMeshProUGUI livescounter;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        currentlives = maxlives;
        SetCountText();
    }

    public void TakeDamage(int amount)
    {
        currenthealth -= amount;

        if (currenthealth <= 0)
        {
            //Dead
            Respawn();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hazard")
        {
            TakeDamage(1);
        }
        if (other.gameObject.tag == "Lava")
        {
            Respawn();
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
        livescounter.text = "Lives " + currentlives.ToString();

    }
    private void Respawn()
    {
            transform.position = startPosition;
        currenthealth = maxhealth;
        currentlives -= 1;
    }
}
