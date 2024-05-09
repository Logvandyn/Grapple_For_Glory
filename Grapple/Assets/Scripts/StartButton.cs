using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public int Scene;
public void sceneSwitch()
    {
        SceneManager.LoadScene(Scene);
    }
}
