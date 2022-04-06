using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseRegenPickup : MonoBehaviour, ICollectible
{
    public static event Action<int> OnBaseRegenCollecter;
    public void Collect()
    {
        Debug.Log("Base Regen Outside Danger Increased!");
        Destroy(gameObject);
        OnBaseRegenCollecter?.Invoke(1);
    }
}
