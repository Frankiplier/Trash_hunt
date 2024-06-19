using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrash : MonoBehaviour
{
    private int rand;
    public Sprite[] pic;

    void Start()
    {
        rand = Random.Range(0, pic.Length);
        GetComponent<SpriteRenderer>().sprite = pic[rand];
    }
}
