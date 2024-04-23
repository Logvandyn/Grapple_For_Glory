using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{


    public GameObject Player;

    public static SceneSwitch instance;
    public GameObject CanvasHolder;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(CanvasHolder);
    }
    public void switchScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
