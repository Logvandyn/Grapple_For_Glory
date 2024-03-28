using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "CheckPoint")
        {
            transform.position = startPosition;
            Blink();
            GetComponent<Transform>().position = startPosition;
        }
        if (other.tag == "Exit")
        {
            SceneSwitch.instance.switchScene(1);
        }
    }
    //cause the player to blink
    public IEnumerator Blink()
    {
        for (int index = 0; index < 20; index++)
        {
            if (index % 2 == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
        GetComponent<MeshRenderer>().enabled = true;
    }
}
