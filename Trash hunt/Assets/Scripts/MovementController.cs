using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] public GameObject icon;
    public bool IsDisplayed = false;

    // normal movement 
    public Rigidbody2D player;
    public float speed;
    private float direction = 0;

    // respawn point and fall detector
    private Vector3 respawnPoint;
    public Transform fallDetector;

    // dashing
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        icon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        // definicja kierunku i zapisywanie w logu
        direction = Input.GetAxis("Horizontal");

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

        // if na dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        // kod na poruszanie sie triggera wraz z postacia
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

        // ify na trigger od spadania i pojawianie sie ikonki interakcji 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            transform.position = respawnPoint;
        }

        if (collision.tag == "Street" && IsDisplayed == false)
        {
            icon.SetActive(true);
            IsDisplayed = true;
        }
    }

        // if na znikanie ikonki interakcji 
    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.tag == "Street")
        {
            icon.SetActive(false);
            IsDisplayed = false;
        } 
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = player.gravityScale;
        player.gravityScale = 0f;
        player.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        player.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}