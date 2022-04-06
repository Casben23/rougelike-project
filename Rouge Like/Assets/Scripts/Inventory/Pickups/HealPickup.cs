using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealPickup : MonoBehaviour, ICollectible
{
    public static event Action<int> OnMaxHealCollected;
    public void Collect()
    {
        Debug.Log("Max Heal Collected!");
        Destroy(gameObject);
        OnMaxHealCollected?.Invoke(20);
    }
}
