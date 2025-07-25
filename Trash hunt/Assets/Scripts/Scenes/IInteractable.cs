using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    GameObject Player { get; set; }

    bool CanInteract { get; set; }

    void Interact();
}
