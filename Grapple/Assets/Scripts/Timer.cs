using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{


    public float timeLeft = 105.0f;
    public TextMeshPro startText; // used for showing countdown from 3, 2, 1 


    void Update()
    {
        timeLeft -= Time.deltaTime;
        /*startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            //insert death screen
        }*/
    }
} 

