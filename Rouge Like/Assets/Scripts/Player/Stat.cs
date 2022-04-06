using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> myModifiers = new List<int>(); 

    public int GetValue()
    {
        int finalValue = baseValue;
        myModifiers.ForEach(m => finalValue += m);
        return finalValue;
    }

    public void AddModifier (int aModifier)
    {
        if(aModifier != 0)
        {
            myModifiers.Add(aModifier);
        }
    }

    public void RemoveModifier(int aModifier)
    {
        if (aModifier != 0)
        {
            myModifiers.Remove(aModifier);
        }
    }
}
