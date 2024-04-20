using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractionBase : MonoBehaviour, IInteractable
{
    public GameObject Player { get; set; }

    public bool CanInteract { get; set; }

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (CanInteract == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            CanInteract = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
        }
    }

    public virtual void Interact() 
    {

    }
}
