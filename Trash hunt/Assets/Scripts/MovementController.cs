using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D player;
    public float speed;
    private float direction = 0;

    private Vector3 respawnPoint;
    public Transform fallDetector;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // definicja kierunku i zapisywanie w logu
        direction = Input.GetAxis("Horizontal");
        Debug.Log(direction);

        // ify na sterowanie postacia lewo-prawo
        if (direction < 0)  
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-0.8f, 0.8f);
        }
        else if (direction > 0)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(0.8f, 0.8f);
        }

        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        // kod na poruszanie sie triggera wraz z postacia
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        }

        // ify na trigger od spadania
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Respawn")
            {
                transform.position = respawnPoint;
            }
    }
}