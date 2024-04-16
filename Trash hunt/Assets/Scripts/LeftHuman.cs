using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHuman : MonoBehaviour
{
    public float moveSpeed;
    public float deadZone;

    void Update()
    {
        transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;

        if (transform.position.x > deadZone)
        {
            Debug.Log("Left human Deleted");
            Destroy(gameObject);
        }
    }
}
