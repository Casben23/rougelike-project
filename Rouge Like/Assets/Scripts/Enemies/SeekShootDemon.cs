using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekShootDemon : MonoBehaviour
{
    public float myHealth;
    GameObject myPlayer;
    private float mySpeed;
    public float myMaxSpeed;
    Rigidbody2D myRb;
    Vector2 moveDir;
    public GameObject myProjectile;
    public Transform myFirePoint;
    Animator myAnimator;
    float timeBtwAttacks;
    private EnemyHealth myEnemyHealth;
    public float myTimeBtwAttacks;
    // Start is called before the first frame update

    
    
    void Start()
    {
        myEnemyHealth = GetComponent<EnemyHealth>();
        timeBtwAttacks = myTimeBtwAttacks;
        myAnimator = this.GetComponent<Animator>();
        mySpeed = myMaxSpeed;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        myRb = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = myPlayer.transform.position - this.transform.position;
        moveDir.Normalize();

        if (myPlayer.transform.position.x > this.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (myPlayer.transform.position.x < this.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        if(timeBtwAttacks <= 0)
        {
            StopToAttack();
            timeBtwAttacks = myTimeBtwAttacks;
        }

        if (myHealth <= 0)
        {
            EnemyManager.Instance.SpawnItem(myRb.transform.position);
            Destroy(gameObject);
        }
        timeBtwAttacks -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (myEnemyHealth != null)
        {
            if (!myEnemyHealth.isStunned())
            {
                myRb.MovePosition(myRb.position + moveDir * mySpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void Shoot()
    {
        Instantiate(myProjectile, myFirePoint.position, Quaternion.FromToRotation(-transform.up, moveDir));
    }

    void StopToAttack()
    {
        myAnimator.SetTrigger("Attack");
        mySpeed = 0;
    }

    public void StartChasing()
    {
        mySpeed = myMaxSpeed;
    }

    public void TakeDamage(float aDmg)
    {
        myHealth -= aDmg;
    }

}
