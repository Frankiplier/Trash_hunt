using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private bool canRead;
    public GameObject tutorial;

    void Start()
    {
        tutorial.SetActive(false);
    }

    void Update()
    {
        if (canRead == true && Input.GetKeyDown(KeyCode.E))
        {
            tutorial.SetActive(true);
        }

        if (canRead == true && Input.GetKeyUp(KeyCode.E))
        {
            tutorial.SetActive(false);
        }
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            canRead = true;
        }
    }

    public void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            canRead = false;
        }
    }
}
