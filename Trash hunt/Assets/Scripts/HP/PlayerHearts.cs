using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHearts : MonoBehaviour
{
    public static event Action OnPlayerChore;
    
    public float hearts, maxHearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = maxHearts;
    }

    public void TakeHearts(int amount)
    {
        hearts -= amount;
        OnPlayerChore?.Invoke();
    }

    public void GiveHearts(int amount)
    {
        hearts += amount;
        OnPlayerChore?.Invoke();
    }
}
