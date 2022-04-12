using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float myMaxHealth;

    private float myCurrentStunTime;
    public float myMaxStunTime;
    private bool myIsStunned;

    public GameObject myDeathEffect;

    public Canvas myHealthBarCanvas;
    public Transform myHealthBarPosition;

    private Rigidbody2D myRb;
    private Slider myHealthBar;
    SpriteRenderer myRenderer;
    float myCurrentHealth;

    private Canvas healthCanvas;
    bool myIsHit;
    void Start()
    {
        myIsStunned = false;
        myRb = GetComponent<Rigidbody2D>();
        healthCanvas = Instantiate(myHealthBarCanvas, myHealthBarPosition.position, Quaternion.identity);
        myHealthBar = healthCanvas.GetComponentInChildren<Slider>();
        myRenderer = GetComponent<SpriteRenderer>();
        myCurrentHealth = myMaxHealth;
        myHealthBar.maxValue = myMaxHealth;
        myHealthBar.minValue = 0;
    }

    void Update()
    {
        myHealthBar.transform.position = myHealthBarPosition.position;
        myHealthBar.value = myCurrentHealth;
        if(myCurrentHealth <= 0)
        {
            Die();
        }

        if(myCurrentStunTime <= 0)
        {
            myIsStunned = false;
        }
        else
        {
            myIsStunned = true;
        }

        myCurrentStunTime -= Time.deltaTime;
    }

    public void TakeDamage(float aDamage, Vector3 aKnockBackDirection)
    {
        myCurrentStunTime = myMaxStunTime;
        myRb.AddForce(aKnockBackDirection, ForceMode2D.Impulse);
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
        GameObject effect = Instantiate(myDeathEffect, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySound(AudioManager.Sound.EnemyDeath, transform.position, false, true);
        Destroy(effect, 1);
        Destroy(healthCanvas);
        Destroy(gameObject);
    }

    public bool isStunned()
    {
        return myIsStunned;
    }

    IEnumerator DoHitEffect()
    {
        myRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        myRenderer.color = Color.white;
        myIsHit = false;
    }
}
