using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStats
{
    private void OnEnable()
    {
        HealPickup.OnMaxHealCollected += AddMaxHealthStat;
        AttackSpeedPickup.OnAttackSpeedCollected += AddAttackSpeedStat;
    }

    private void OnDisable()
    {
        HealPickup.OnMaxHealCollected -= AddMaxHealthStat;
        AttackSpeedPickup.OnAttackSpeedCollected -= AddAttackSpeedStat;
    }

    public void TakeDamage(int aDamage)
    {
        myCurrentHealth -= aDamage;
        if(myCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    #region Subscribed Functions

    private void AddMaxHealthStat(int aMaxHealthAmount)
    {
        myMaxHealth.AddModifier(aMaxHealthAmount);
    }

    private void AddAttackSpeedStat(int aAttackSpeedAmount)
    {
        myAttackSpeed.AddModifier(aAttackSpeedAmount);
    }

    private void AddMoveSpeedStat()
    {
    }
    private void ProjectileDealFireDamage()
    {
    }

    #endregion
}
