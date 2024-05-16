using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    public static SceneSwapManager instance;
    [SerializeField] public GameObject icon;
    [SerializeField] public GameObject gameOver;

    // bools
    public bool IsDisplayed = false;
    public bool canHide = true;
    public bool canDark = false;
    public bool IsHiding = false;
    private bool canJump = true;

    // normal movement 
    public Rigidbody2D player;
    public FloatContainer speed;
    private float direction = 0f;

    // respawn point and fall detector
    private Vector3 respawnPoint;
    public Transform fallDetector;

    //jumping
    public float jumpStrenght;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    private bool isTouchingGround = true;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    // dashing
    public bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    // score
    public Text scoreText;
    public int score = 0;

    // fading
    private float tpTime = 0.2f;
    private float fadeTime = 1f;

    void OnDisable()
    {
        speed.variable = 0f;
    }

    void Start()
    {
        Time.timeScale = 1f;
        gameOver.SetActive(false);

        player = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        icon.SetActive(false);
        speed.variable = 3f;
        scoreText.text = score.ToString();
    }

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
            player.velocity = new Vector2(direction * speed.variable, player.velocity.y);
            transform.localScale = new Vector2(-0.8f, 0.8f);
        }
        else if (direction > 0)
        {
            player.velocity = new Vector2(direction * speed.variable, player.velocity.y);
            transform.localScale = new Vector2(0.8f, 0.8f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        // definicja groundcheckingu
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ify na skakanie z ground checkiem
        if (isTouchingGround == true && canJump == true)
        {
            if ((Input.GetKeyDown(KeyCode.Space)))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                player.velocity = new Vector2(player.velocity.x, jumpStrenght);
            }
        }

        // ify na wyzszy skok przy przytrzymaniu klawisza - ma byc samo "GetKey"!!!
        if ((Input.GetKey(KeyCode.Space)) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                player.velocity = new Vector2(player.velocity.x, jumpStrenght);
                jumpTimeCounter -= Time.deltaTime;
            }

            else 
            {
                isJumping = false;
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // if na dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && IsHiding == false && isTouchingGround == true)
        {
            StartCoroutine(Dash());
        }

        // kod na poruszanie sie triggera wraz z postacia
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

        // ify na ukrywanie sie
        if (canHide == true && Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed.variable = 1.5f;
            IsHiding = true;
            canDash = false;
            canJump = false;
        }
        else if (canHide == true && Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed.variable = 3f;
            IsHiding = false;
            canDash = true;
            canJump = true;
        }
        else if (canHide == false)
        {
            speed.variable = 3f;
            IsHiding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Respawn")
        {
            transform.position = respawnPoint;
        }

        if (other.tag == "Street")
        {
            icon.SetActive(true);
            IsDisplayed = true;
            
        }

        if (other.tag == "Light")
        {
            canHide = false;
        }

        if (other.tag == "Lamp")
        {
            canDark = true;

            icon.SetActive(true);
            IsDisplayed = true;
        }

        if (other.tag == "Trash")
        {
            icon.SetActive(true);
            IsDisplayed = true;
        }

        if (other.tag == "Trashcan")
        {
            icon.SetActive(true);
            IsDisplayed = true;
        }

        if (other.tag == "Busted")
        {
            HealthManager.health--;
            
            StartCoroutine(Fade());

            if (HealthManager.health <= 0)
            {
                gameOver.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Street")
        {
            icon.SetActive(false);
            IsDisplayed = false;
        }

        if (other.tag == "Light")
        {
            canHide = true;
        }

        if (other.tag == "Lamp")
        {
            canDark = false;

            icon.SetActive(false);
            IsDisplayed = false;
        }

        if (other.tag == "Trash")
        {
            icon.SetActive(false);
            IsDisplayed = false;
        }

        if (other.tag == "Trashcan")
        {
            icon.SetActive(false);
            IsDisplayed = false;
        }
    }

    public void LampTurnedOff()
    {
        icon.SetActive(false);
        IsDisplayed = false;

        canHide = true;
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

    private IEnumerator Fade()
    {
        SceneFadeManager.instance.StartFadeOut();
        yield return new WaitForSeconds(tpTime);
        SceneManager.LoadSceneAsync(0);
        transform.position = respawnPoint;
        yield return new WaitForSeconds(fadeTime);
        SceneFadeManager.instance.StartFadeIn();
    }
}