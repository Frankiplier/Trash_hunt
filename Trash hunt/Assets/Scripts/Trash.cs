using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public static Trash Instance;

    public GameObject trash;
    MovementController player;
    private bool canTrash = false;
    private bool isVisible;

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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
