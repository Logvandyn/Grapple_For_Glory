using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
public void sceneSwitch()
    {
        SceneManager.LoadScene(1);
    }
}
