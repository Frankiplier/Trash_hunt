using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTrashcan : MonoBehaviour
{
    MovementController player;
    public Sprite niu, old;
    private bool isTrash = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        GetComponent<SpriteRenderer>().sprite = niu;
    }

    void Update()
    {
        if (isTrash == true && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Nie ma");
                GetComponent<SpriteRenderer>().sprite = old;
            }
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player") 
        {
            isTrash = true; 
        }
    }

    public void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player") 
        {
            isTrash = false; 
        }
    }
}
