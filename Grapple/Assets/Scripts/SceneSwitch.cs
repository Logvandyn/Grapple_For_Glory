using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{


    public GameObject Player;
    public GameObject mainCamera;

    public static SceneSwitch instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(this.gameObject);
    }
    public void switchScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
