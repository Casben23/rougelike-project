using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int myCurrentHealth;
    public Stat myMaxHealth;
    public Stat myDamage;
    public Stat myAttackSpeed;
    public Stat myMoveSpeed;
    public Stat myProjectileSpeed;

    private void Awake()
    {
        myCurrentHealth = myMaxHealth.GetValue();
    }
}
