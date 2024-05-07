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
        else
        {
            timeLeft = Time.deltaTime;
        }
           
        
    }
   
} 

