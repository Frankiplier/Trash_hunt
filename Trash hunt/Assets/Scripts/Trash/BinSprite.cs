using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/BinSprite")]

public class CheckedBinsList : ScriptableObject
{
    public List<bool> checkedAllBins = new List<bool>();

    public void ResetSprites()
    {
        for (int i = 0; i < checkedAllBins.Count; i++)
            checkedAllBins[i] = false;
    }
}
