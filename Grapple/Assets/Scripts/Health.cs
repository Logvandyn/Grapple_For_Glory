using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxhealth = 3;
    public int currenthealth;
    public int maxlives = 3;
    public int currentlives;
    public int amount;
    public Vector3 startPosition; //for spawning / respawning
    public TextMeshProUGUI healthcounter;
    public TextMeshProUGUI livescounter;
    public TextMeshProUGUI lose;
    public TextMeshProUGUI win;
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

    public void Heal()
    {
        currenthealth = maxhealth; //restore health to full
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
        if (other.gameObject.tag == "Kill")
        {
            Respawn();
        }
        if (other.gameObject.tag == "Win")
        {
            win.text = "YOU WIN!"; //only enable this on last level cause it shows up the whole time
            GetComponent<PlayerMovement>().enabled = false;
            //GetComponent<SceneSwitch>().switchScene(1);
            //SceneManager.LoadScene(1);
        }
        if (other.gameObject.tag == "Portal")
        {
            //win.text = "YOU WIN!"; //only enable this on last level cause it shows up the whole time
            //GetComponent<PlayerMovement>().enabled = false;
            //GetComponent<SceneSwitch>().switchScene(1);
            SceneManager.LoadScene(1);
        }
        //heal
        if (other.gameObject.tag == "Health") //other, not collision
        {
            Heal();
        }

        //super scuffed way for set spawn
        if (other.gameObject.tag == "Spawnpoint") //this is gonna be so stupid if it works
        {
            startPosition = this.gameObject.transform.position;
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
        if (currentlives <= 0)
        {
            lose.text = "GAME OVER";
            GetComponent<PlayerMovement>().enabled = false;
        }

    }
    private void Respawn()
    {
            transform.position = startPosition; 
        currenthealth = maxhealth;
        currentlives -= 1;
    }
}
