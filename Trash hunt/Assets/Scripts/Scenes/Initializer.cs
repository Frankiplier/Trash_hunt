using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Initializer 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void Execute()
    {
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSIST OBJECTS")));
    }
}
