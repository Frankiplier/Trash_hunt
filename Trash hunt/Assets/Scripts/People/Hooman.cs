using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooman : MonoBehaviour
{
     [SerializeField] FloatContainer speed;

    [SerializeField] public GameObject green;
    [SerializeField] public GameObject yellow;
    [SerializeField] public GameObject red;

    // Start is called before the first frame update
    void Start()
    {
        green.SetActive(true);
        yellow.SetActive(false);
        red.SetActive(false);
    }

    void Update()
    {
        // ify na ukrywanie sie
        if (speed.variable == 1.5f) 
        {
            yellow.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (speed.variable == 3f) 
        {
            yellow.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            green.SetActive(false);
            yellow.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            green.SetActive(true);
            yellow.SetActive(false);
        }
    }
}
