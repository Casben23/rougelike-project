using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float myMaxHealth;

    public Canvas myHealthBarCanvas;
    public Transform myHealthBarPosition;

    private Slider myHealthBar;
    SpriteRenderer myRenderer;
    float myCurrentHealth;

    private Canvas healthCanvas;
    bool myIsHit;
    void Start()
    {
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
        Destroy(healthCanvas);
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
