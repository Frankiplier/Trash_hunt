using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    [SerializeField] public GameObject icon;
    [SerializeField] public GameObject green;
    [SerializeField] public GameObject yellow;
    [SerializeField] public GameObject red;
    [SerializeField] public GameObject busted;
    [SerializeField] public GameObject flash;

    // bools
    public bool IsDisplayed = false;
    private bool canHide = true;
    private bool canDark = false;

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
        green.SetActive(true);
        yellow.SetActive(false);
        red.SetActive(false);
        flash.SetActive(true);
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

        // ify na ukrywanie sie
        if (canHide == true && Input.GetKeyDown(KeyCode.LeftControl))
        {
            busted.SetActive(false);
            speed = 1.5f;
        }
        else if (canHide == true && Input.GetKeyUp(KeyCode.LeftControl))
        {
            busted.SetActive(true);
            speed = 3f;
        }
        else if (canHide == false)
        {
            busted.SetActive(true);
            speed = 3f;
        }

        // if na wylaczanie swiatla
        if (canDark == true && Input.GetKeyDown(KeyCode.E))
        {
            flash.SetActive(false);

            StartCoroutine(WaitBeforeLight());
        }
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

        if (collision.tag == "Light")
        {
            icon.SetActive(true);
            IsDisplayed = true;

            canHide = false;
            canDark = true;
        }

        if (collision.tag == "Human")
        {
            green.SetActive(false);
            yellow.SetActive(true);
        }

        if (collision.tag == "Red")
        {
            green.SetActive(false);
            yellow.SetActive(false);
            red.SetActive(true);
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

        if (collision.tag == "Light")
        {
            icon.SetActive(false);
            IsDisplayed = false;

            canHide = true;
            canDark = false;
        } 

        if (collision.tag == "Human")
        {
            yellow.SetActive(false);
            green.SetActive(true);
        } 

        if (collision.tag == "Red")
        {
            red.SetActive(false);
            yellow.SetActive(true);
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

    private IEnumerator WaitBeforeLight()
    {
        yield return new WaitForSeconds(10);

        flash.SetActive(true);
    }
}