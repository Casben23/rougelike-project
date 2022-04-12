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
    public Stat myBaseHealthRegen;
    public Stat myChargeSpeed;
    public Stat myChargingMoveSpeed;
    public Stat myCritChance;
    public Stat myKnockBackForce;
    private void Awake()
    {
        myCurrentHealth = myMaxHealth.GetValue();
    }

    private void Update()
    {
        
    }
    
     
}
