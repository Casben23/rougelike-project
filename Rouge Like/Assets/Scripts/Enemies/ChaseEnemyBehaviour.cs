using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyBehaviour : MonoBehaviour
{
    public float myHealth;
    GameObject myPlayer;
    private float mySpeed;
    public float myMaxSpeed;
    Rigidbody2D myRb;
    Vector2 moveDir;
    public float myDamage = 5;
    private EnemyHealth myEnemyHealth;

    private IEnumerator couroutine;
    // Start is called before the first frame update
    void Start()
    {
        myEnemyHealth = GetComponent<EnemyHealth>();
        mySpeed = myMaxSpeed;
        if(GameObject.FindGameObjectWithTag("Player") != null)
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

        if(myHealth <= 0)
        {
            EnemyManager.Instance.SpawnItem(myRb.transform.position);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(myEnemyHealth != null)
        {
            if (!myEnemyHealth.isStunned())
            {
                myRb.MovePosition(myRb.position + moveDir * mySpeed * Time.fixedDeltaTime);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.SendMessage("TakeDamage", myDamage, SendMessageOptions.DontRequireReceiver);
            mySpeed = 0;
            Invoke("StartChasing", 2);
        }
    }

    void StartChasing()
    {
        mySpeed = myMaxSpeed;
    }
}
