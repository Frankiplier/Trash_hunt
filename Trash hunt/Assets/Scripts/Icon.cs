using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    private float direction = 0f;

    void Update()
    {
        direction = Input.GetAxis("Horizontal");

        if (direction < 0)  
        {
            transform.localScale = new Vector2(-0.75f, 0.75f);
        }

        else if (direction > 0)
        {
            transform.localScale = new Vector2(0.75f, 0.75f);
        }
    }
}
