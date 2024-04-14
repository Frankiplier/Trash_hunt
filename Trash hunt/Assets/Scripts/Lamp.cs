using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] public GameObject flash;
    public GameObject lamp;

    void Start()
    {
        flash.SetActive(true);
    }

    public void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "Player") 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                flash.SetActive(false);
                lamp.GetComponent<BoxCollider2D>().enabled = false;
    
                StartCoroutine(WaitBeforeLight());
            }
        }
    }

    private IEnumerator WaitBeforeLight()
    {
        yield return new WaitForSeconds(10);

        flash.SetActive(true);
        lamp.GetComponent<BoxCollider2D>().enabled = true;
    }
}
