using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekProjectile : MonoBehaviour
{
    public float mySpeed = 10;
    public float myRotationSpeed = 10;
    private Rigidbody2D myRb;
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
}
