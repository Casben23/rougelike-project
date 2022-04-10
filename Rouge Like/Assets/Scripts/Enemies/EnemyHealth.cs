using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float myMaxHealth;

    SpriteRenderer myRenderer;
    float myCurrentHealth;

    bool myIsHit;
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
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
        if (!myIsHit)
        {
            myIsHit = true;
            StartCoroutine("DoHitEffect");
        }
    }

    void Die()
    {
        EnemyManager.Instance.SpawnItem(transform.position);
        EnemyManager.Instance.OnEnemyDeath();
        Destroy(gameObject);
    }

    IEnumerator DoHitEffect()
    {
        myRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        myRenderer.color = Color.white;
        myIsHit = false;
    }
}
