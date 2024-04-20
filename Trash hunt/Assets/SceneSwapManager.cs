using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;

    private GameObject _player;
    private Collider2D _playerCollider;
    private Collider2D _streetCollider;
    private Vector3 _playerSpawnPosition;

    private static bool _loadFromStreet;

    private StreetTriggerInteraction.StreetToSpawnAt _streetToSpawnTo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _player.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void SwapSceneFromStreetUse(SceneField myScene, StreetTriggerInteraction.StreetToSpawnAt streetToSpawnAt)
    {
        _loadFromStreet = true;
        instance.StartCoroutine(instance.FadeOutThenChangeScene(myScene, streetToSpawnAt));
    }

    private IEnumerator FadeOutThenChangeScene(SceneField myScene, StreetTriggerInteraction.StreetToSpawnAt streetToSpawnAt = StreetTriggerInteraction.StreetToSpawnAt.None)
    {
        SceneFadeManager.instance.StartFadeOut();

        while(SceneFadeManager.instance.IsFadingOut)
        {
            yield return null;
        }

        _streetToSpawnTo = streetToSpawnAt;
        SceneManager.LoadScene(myScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneFadeManager.instance.StartFadeIn();

        if (_loadFromStreet)
        {
            FindStreet(_streetToSpawnTo);
            _player.transform.position = _playerSpawnPosition;

            _loadFromStreet = false;
        }
    }

    private void FindStreet(StreetTriggerInteraction.StreetToSpawnAt streetSpawnNumber)
    {
        StreetTriggerInteraction[] streets = FindObjectsOfType<StreetTriggerInteraction>();

        for (int i = 0; i < streets.Length; i++)
        {
            if (streets[i].CurrentStreetPosition == streetSpawnNumber)
            {
                _streetCollider = streets[i].gameObject.GetComponent<Collider2D>();

                CalculateSpawnPosition();

                return;
            }
        }
    }

    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerCollider.bounds.extents.y;

        _playerSpawnPosition = _streetCollider.transform.position - new Vector3(0f, 7 * colliderHeight, 0f);
    }
}
