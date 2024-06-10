using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] PickedTrashList pickedTrash;
    [SerializeField] int index;
    public GameObject trash;
    MovementController player;

    private bool canTrash = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
    }

    void Update()
    {
        if (canTrash == true && Input.GetKeyDown(KeyCode.E))
            {
                trash.SetActive(false);
                player.score += 1;
                player.scoreText.text = player.score.ToString();
                pickedTrash.pickedUpTrash[index] = true;
            }
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player") 
        {
            canTrash = true; 
        }
    }

    public void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player") 
        {
            canTrash = false; 
        }
    }

    private void Awake()
    {
        if (pickedTrash.pickedUpTrash[index])
        {
            Destroy(gameObject);
            return;
        }
    }
}
