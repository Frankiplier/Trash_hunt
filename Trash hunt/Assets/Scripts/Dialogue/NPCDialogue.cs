using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    private bool canTalk;
    public float timesInteracted = 1;
    private SpriteRenderer speechBubble;
    public DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        speechBubble = GetComponent<SpriteRenderer>();
        speechBubble.enabled = false;
        canTalk = false;
    }

    void Update()
    {
        if (canTalk == true)
        {
            if (timesInteracted == 1 && Input.GetKeyDown(KeyCode.E))
            {
                dialogueTrigger.TriggerDialogue();
                timesInteracted--;
            }

            if (timesInteracted == 0 && Input.GetKeyDown(KeyCode.Return))
            {
                DialogueManager.Instance.DisplayNextDialogueLine();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canTalk = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (timesInteracted == 1)
            {
                speechBubble.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            speechBubble.enabled = false;
            canTalk = false;
        }
    }
}
