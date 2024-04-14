using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    [SerializeField] public GameObject icon;
    public bool isDisplayed = false;

    void Start()
    {
        icon.SetActive(false);
    }

    public void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "Player") 
        {
            icon.SetActive(true);
            isDisplayed = true;

            if (Input.GetKeyDown(KeyCode.E) && isDisplayed == true)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
           icon.SetActive(false);
           isDisplayed = false; 
        }
    }
}
