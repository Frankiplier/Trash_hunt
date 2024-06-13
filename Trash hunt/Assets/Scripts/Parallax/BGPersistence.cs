using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGPersistence : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
