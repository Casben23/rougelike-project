using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStats
{
    [SerializeField]
    private float myTimeBtwRegenHeal;
    private float myRegenTime;

    private void OnEnable()
    {
        HealPickup.OnMaxHealCollected += AddMaxHealthStat;
        AttackSpeedPickup.OnAttackSpeedCollected += AddAttackSpeedStat;
        BaseRegenPickup.OnBaseRegenCollecter += AddBaseRegenStat;
    }

    private void OnDisable()
    {
        HealPickup.OnMaxHealCollected -= AddMaxHealthStat;
        AttackSpeedPickup.OnAttackSpeedCollected -= AddAttackSpeedStat;
        BaseRegenPickup.OnBaseRegenCollecter -= AddBaseRegenStat;
    }

    public void Update()
    {
        HealWithRegen();
    }

    public int GetHealth()
    {
        return myCurrentHealth;
    }

    public void TakeDamage(int aDamage)
    {
        myCurrentHealth -= aDamage;
        if(myCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
        AudioManager.Instance.PlaySound(AudioManager.Sound.PlayerHit, transform.position, false, true);
    }

    void HealWithRegen()
    {
        myRegenTime -= Time.deltaTime;
        if(myRegenTime <= 0)
        {
            myCurrentHealth += myBaseHealthRegen.GetValue();
            myRegenTime = myTimeBtwRegenHeal;
        }

        if (myCurrentHealth > myMaxHealth.GetValue())
        {
            myCurrentHealth = myMaxHealth.GetValue();
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

    private void AddBaseRegenStat(int aAmount)
    {
        myBaseHealthRegen.AddModifier(aAmount);
    }

    private void AddMoveSpeedStat()
    {
    }
    private void ProjectileDealFireDamage()
    {
    }

    #endregion
}
