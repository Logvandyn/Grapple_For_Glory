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
    public Vector3 spawnpos = new Vector3(3, 10, 262);

    //timer
    public float timeLeft = 105.0f;
    public TextMeshProUGUI timeText; // used for showing countdown from 3, 2, 1 


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
        GetComponent<GrappleSwing>().lr.enabled = false;
        GetComponent<GrappleSwing>().StopSwing();

        if (currenthealth <= 0)
        {
            //Dead
            Respawn();
            
        }
    }

    public void Heal()
    {
        if (currenthealth == maxhealth)
        {
            currentlives += 1; //add a life
        }
        else
        {
            currenthealth = maxhealth; //restore health to full
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

            //fix start pos from checkpoints

            SceneManager.LoadScene(1);
            //SpawnReset();
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

        //time pickup
        if (other.gameObject.tag == "Clock") 
        {
            timeLeft += 30f;
        }

    }
 
   
        // Update is called once per frame
        void Update()
    {
        SetCountText();
        //timer
        //timeLeft -= Time.deltaTime;
        //timeLeft -= 1;
        //doesnt call text update anywhere

        //does it have to be fixed update?
        //A: No it doesnt

        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
            //timeLeft -= 1;
            //doesnt call text update anywhere
            timeText.text = "TIME LEFT: " + timeLeft.ToString();
        }
        //game over
        if (timeLeft <= 0)
        {
            GameOver();
            timeText.text = "TIME LEFT: NONE, YOU DIDN'T MAKE IT";
        }
    }

    /*
    private void FixedUpdate() //HANDLED IN NORMAL UPDATE
    {
        //timeLeft -= Time.deltaTime;
            //timeLeft -= 1;
            //doesnt call text update anywhere
        //timeText.text = "TIME LEFT: " + timeLeft.ToString();
    }
    */

    public void SetCountText()
    {
        //counts the Health you have left
        healthcounter.text = "Health " + currenthealth.ToString();
        livescounter.text = "Lives " + currentlives.ToString();
        if (currentlives <= 0)
        {
            GameOver();
        }

    }
    private void Respawn()
    {
            transform.position = startPosition; 
        currenthealth = maxhealth;
        currentlives -= 1;

        GetComponent<GrappleSwing>().lr.enabled = false; //stop grappling
        GetComponent<GrappleSwing>().StopSwing();
    }

    public void GameOver()
    {
        lose.text = "GAME OVER";
        GetComponent<PlayerMovement>().enabled = false;
    }

    /*
    public void SpawnReset()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnpos;
    }
    */
}
