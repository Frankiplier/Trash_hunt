using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullTrashcan : MonoBehaviour
{
    [SerializeField] public GameObject trash;
    public Sprite full, empty;
    MovementController player;
    private bool isTrash = false;
    private bool isEmpty = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        GetComponent<SpriteRenderer>().sprite = full;
        trash.SetActive(false);
    }

    void Update()
    {
        if (isTrash == true && isEmpty == false && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<SpriteRenderer>().sprite = empty;
            
            trash.SetActive(true);
            isEmpty = true;
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
