using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowLeftSpawner : MonoBehaviour
{
    public GameObject human;
    private float targetTime;

    // Start is called before the first frame update
    void Start()
    {
        targetTime = 6;
    }

    // Update is called once per frame
     private void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0)
        {
            SpawnHuman();

            targetTime = Random.Range(15, 30);
        }
    }

    void SpawnHuman()
    {
        Instantiate(human, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}
