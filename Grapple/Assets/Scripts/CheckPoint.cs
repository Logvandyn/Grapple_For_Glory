using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private Vector3 startPosition;
    public GameObject player;

    private void Start()
    {
        //startPosition = transform.position;
        //transform.position = startPosition; //flip em
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "CheckPoint")
        {
            //transform.position = startPosition;
            //needs the player
            GetComponent<Health>().startPosition = transform.position; //player start = checkpoint pos
            //startPosition = transform.position; //flip em
            Blink();
            //player.GetComponent<Transform>().position = startPosition; //startpos needs to be in Health not in CheckPoint

            //GameObject.FindGameObjectWithTag("Player").transform.position = gameObject.transform.position;
        }
        if (other.tag == "Exit")
        {
            //SceneSwitch.instance.switchScene(1);
        }
    }
    //cause the player to blink
    public IEnumerator Blink()
    {
        for (int index = 0; index < 20; index++)
        {
            if (index % 2 == 0)
            {
                player.GetComponent<MeshRenderer>().enabled = false; //needs to be the player
            }
            else
            {
                player.GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
        GetComponent<MeshRenderer>().enabled = true;
    }
}
