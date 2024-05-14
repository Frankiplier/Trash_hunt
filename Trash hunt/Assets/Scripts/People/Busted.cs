using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Busted : MonoBehaviour
{
    [SerializeField] public GameObject yellow;
    [SerializeField] public GameObject red;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            yellow.SetActive(false);
            red.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            red.SetActive(false);
        }
    }
}
