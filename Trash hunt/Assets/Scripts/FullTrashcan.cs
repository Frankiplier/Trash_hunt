using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullTrashcan : MonoBehaviour
{
    [SerializeField] public GameObject trash;
    MovementController player;
    private bool isTrash = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        trash.SetActive(false);
    }

    void Update()
    {
        if (isTrash == true && Input.GetKeyDown(KeyCode.E))
            {
                trash.SetActive(true);
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
