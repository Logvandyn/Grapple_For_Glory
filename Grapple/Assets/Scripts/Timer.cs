using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public bool timerRunning;
    public float timeLeft = 100.0f;
   // public TextMeshProUGUI startText; // used for showing countdown from 3, 2, 1 


    void Update()
    {
        if (timerRunning == true)
        {
            timeLeft -= Time.deltaTime;
        }
        
           
        /*startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            //insert death screen
        }*/
    }
   
} 

