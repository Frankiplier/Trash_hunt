using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartsBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHearts playerHearts;
    List<Hearts> hert = new List<Hearts>();

    void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        int heartsToMake = (int)((playerHearts.maxHearts));

        for (int i=0; i < heartsToMake; i++)
        {
            CreateEmptyHearts();
        }

        for (int i=0; i < hert.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHearts.hearts - i, 0, 1);
            hert[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHearts()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        Hearts heartComponent = newHeart.GetComponent<Hearts>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hert.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        hert = new List<Hearts>();
    }

    private void OnEnable()
    {
        PlayerHearts.OnPlayerChore += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerHearts.OnPlayerChore -= DrawHearts;
    }
}