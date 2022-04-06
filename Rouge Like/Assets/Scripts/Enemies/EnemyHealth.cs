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
        myCurrentHealth -= aDamage;
    }

    void Die()
    {
        EnemyManager.Instance.SpawnItem(transform.position);
        Destroy(gameObject);
    }
}
