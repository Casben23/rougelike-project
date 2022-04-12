using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AttackSpeedPickup : MonoBehaviour, ICollectible
{
    public static event Action<int> OnAttackSpeedCollected; 
    public void Collect()
    {
        Debug.Log("Attack Speed Collected!");
        Destroy(gameObject);
        OnAttackSpeedCollected?.Invoke(1);
    }
}
