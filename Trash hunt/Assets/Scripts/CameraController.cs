using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float offset;
    public float offsetSmoothing;
    private Vector3 targetPosition;

    // do smieci
    [SerializeField] PickedTrashList trashList;
    [SerializeField] CheckedBinsList binsList;

    void Start()
    {
        trashList.ResetList();
        binsList.ResetSprites();
    }

    // Update is called once per frame
    void Update()
    {
        // locking camera on a target
        targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

        // ify na smooth przejscia kamera
        if(target.localScale.x > 0)
        {
            targetPosition = new Vector3(targetPosition.x + offset, targetPosition.y, targetPosition.z);
        }
        else
        {
            targetPosition = new Vector3(targetPosition.x - offset, targetPosition.y, targetPosition.z);
        }

        // kod na czas reakcji kamery
        transform.position = Vector3.Lerp(transform.position, targetPosition, offsetSmoothing * Time.deltaTime);
    }
}
