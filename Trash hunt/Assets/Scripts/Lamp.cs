using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] public GameObject flash;
    public GameObject lamp;
    MovementController player;
    private bool canTurn = false;

    void Start()
    {
        flash.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
    }

    void Update()
    {
        if (player.canDark == true && canTurn == true && Input.GetKeyDown(KeyCode.E))
            {
                flash.SetActive(false);
                lamp.GetComponent<BoxCollider2D>().enabled = false;
                transform.gameObject.tag = "Troll";

                player.LampTurnedOff();
    
                StartCoroutine(WaitBeforeLight());
            }
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player") 
        {
            canTurn = true; 
        }
    }

    public void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player") 
        {
            canTurn = false; 
        }
    }

    private IEnumerator WaitBeforeLight()
    {
        yield return new WaitForSeconds(10);

        flash.SetActive(true);
        lamp.GetComponent<BoxCollider2D>().enabled = true;

        transform.gameObject.tag = "Lamp";
    }
}
