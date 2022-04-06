using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    #region Singleton Logic
    private static PlayerStatsManager _instance;

    public static PlayerStatsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerStatsManager>();
            }

            return _instance;
        }
    }
    #endregion

    public struct PlayerStats
    {
        public float myMaxHealth;

        public float myCurrentHealth;

        public float myMoveSpeed;
    }

    public struct ProjectileStats
    {
        public bool myDoingFireDamage;

        public float myFireDamage;

        public float myIsExplosive;

        public float myExplosionRadius;

        public float myExplosionDamage;
    }

    public struct WeaponStats
    {
        public float myAttackSpeed;

        public bool myWeaponStatChanged;
    }

    WeaponStats myWeaponStats = new WeaponStats();
    PlayerStats myPlayerStats = new PlayerStats();
    ProjectileStats myProjectileStats = new ProjectileStats();


    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        myWeaponStats = new WeaponStats();
        myPlayerStats = new PlayerStats();
        myProjectileStats = new ProjectileStats();
    }

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

    #region Subscribed Functions

    private void AddMaxHealthStat()
    {
        myPlayerStats.myMaxHealth += 40;
    }

    private void AddAttackSpeedStat()
    {
        myWeaponStats.myAttackSpeed += 0.5f;
    }

    private void AddMoveSpeedStat()
    {
        myPlayerStats.myMoveSpeed += 2;
    }
    private void ProjectileDealFireDamage()
    {
        myProjectileStats.myDoingFireDamage = true;
    }

    #endregion
}
