using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float myMaxHealth;

    float myCurrentHealth;

    void Start()
    {
        myCurrentHealth = myMaxHealth;
    }

    void Update()
    {
        if(myCurrentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float aDamage)
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.EnemyHit, transform.position, false, true);
        myCurrentHealth -= aDamage;
    }

    void Die()
    {
        EnemyManager.Instance.SpawnItem(transform.position);
        EnemyManager.Instance.OnEnemyDeath();
        Destroy(gameObject);
    }
}
