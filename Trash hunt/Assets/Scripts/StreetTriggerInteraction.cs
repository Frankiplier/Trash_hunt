using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetTriggerInteraction : TriggerInteractionBase
{
    public enum StreetToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four,
    }

    [Header("Spawn TO")]
    [SerializeField] private StreetToSpawnAt StreetToSpawnTo;
    [SerializeField] private SceneField _sceneToLoad;

    [Space(10f)]
    [Header("THIS Street")]
    public StreetToSpawnAt CurrentStreetPosition;
    public override void Interact()
    {
        SceneSwapManager.SwapSceneFromStreetUse(_sceneToLoad, StreetToSpawnTo);
    }
}
