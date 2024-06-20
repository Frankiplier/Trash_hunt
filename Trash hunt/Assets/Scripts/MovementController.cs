using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public static SceneSwapManager instance;
    [SerializeField] public GameObject icon;
    [SerializeField] public GameObject gameOver;
    public GameObject trashIcon;
    public GameObject scoreEnd;
    public GameObject pause;

    // bools
    public bool IsDisplayed = false;
    public bool canHide = true;
    public bool canDark = false;
    public bool IsHiding = false;
    private bool canTrash = false;

    // normal movement 
    public Rigidbody2D player;
    public FloatContainer speed;
    private float direction = 0f;

    // respawn point and fall detector
    private Vector3 respawnPoint;
    public Transform fallDetector;

    //groundcheck
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    private bool isTouchingGround = true;

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
        speed.variable = 3.5f;
        scoreText.text = score.ToString();

        animator.GetComponent<Animator>();
    }

    void Update()
    {
        // kiedy jaka animacja
        animator.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        animator.SetBool("OnGround", isTouchingGround);
        animator.SetBool("IsHiding", IsHiding);

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

        // if na dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && IsHiding == false && isTouchingGround == true)
        {
            StartCoroutine(Dash());
        }

        // if na animacje kopania
        if (Input.GetKeyDown(KeyCode.E) && canDark == true)
        {
            animator.SetTrigger("Kick");
        }

        // if na animacje zbierania smieci
        if (Input.GetKeyDown(KeyCode.E) && canTrash == true)
        {
            animator.SetTrigger("PickUpTrash");
        }

        // kod na poruszanie sie triggera wraz z postacia
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

        // ify na ukrywanie sie
        if (canHide == true && Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed.variable = 1.5f;
            IsHiding = true;
            canDash = false;
        }
        else if (canHide == true && Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed.variable = 3.5f;
            IsHiding = false;
            canDash = true;
        }
        else if (canHide == false)
        {
            speed.variable = 3.5f;
            IsHiding = false;
        }

        // if na zamkniecie gry po zostaniu zlapanym
        if (PauseMenu.isPaused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Respawn")
        {
            transform.position = respawnPoint;
        }

        if (other.tag == "Tutorial")
        {
            icon.SetActive(true);
            IsDisplayed = true;
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
            canTrash = true;

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
                trashIcon.SetActive(false);
                scoreEnd.SetActive(false);
                pause.SetActive(false);

                Time.timeScale = 0f;
                PauseMenu.canPause = false;
                PauseMenu.isPaused = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Tutorial")
        {
            icon.SetActive(false);
            IsDisplayed = false;
        }

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
            canTrash = false;

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

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator Dash()
    {
        audioSource.Play();
        animator.SetTrigger("Dash");
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
        SceneManager.LoadSceneAsync(1);
        transform.position = respawnPoint;
        yield return new WaitForSeconds(fadeTime);
        SceneFadeManager.instance.StartFadeIn();
    }
}