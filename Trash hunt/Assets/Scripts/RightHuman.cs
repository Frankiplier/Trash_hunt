using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHuman : MonoBehaviour
{
    public float moveSpeed;
    public float deadZone;

    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("RIght human deleted");
            Destroy(gameObject);
        }
    }
}
