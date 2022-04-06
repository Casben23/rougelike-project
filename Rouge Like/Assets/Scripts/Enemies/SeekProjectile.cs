using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekProjectile : MonoBehaviour
{
    public float mySpeed = 10;
    public float myRotationSpeed = 10;
    private Rigidbody2D myRb;
    public float myDamage = 4;
    public Transform myPlayer;
    public float myTimeAlive = 3;
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        myRb = this.GetComponent<Rigidbody2D>();
        Destroy(gameObject, myTimeAlive);
    }

    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)myPlayer.position - myRb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        myRb.angularVelocity = rotateAmount * myRotationSpeed;
        myRb.velocity = -transform.up * mySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Environment"))
        {
            collision.gameObject.SendMessage("TakeDamage", myDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.SendMessage("TakeDamage", myDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Environment"))
        {
            collision.gameObject.SendMessage("TakeDamage", myDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.SendMessage("TakeDamage", myDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
