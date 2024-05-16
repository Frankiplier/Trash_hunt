using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Bin")]

public class PickedTrashList : ScriptableObject
{
    public List<bool> pickedUpTrash = new List<bool>();

    public void ResetList()
    {
        for (int i = 0; i < pickedUpTrash.Count; i++)
            pickedUpTrash[i] = false;
    }
}
