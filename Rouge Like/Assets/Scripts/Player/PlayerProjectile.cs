using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D myRb;
    public float mySpeed;
    private float myShotAngle;
    Vector3 myShootDir;
    public float myDamage;

    public void SetBullet(float aDmg, float aSpeed)
    {
        myDamage = aDmg;
        mySpeed = aSpeed;
        Shoot();
    }

    void Shoot()
    {
        myShotAngle = Mathf.Atan2(Input.GetAxis("RightJoyY"), Input.GetAxis("RightJoyX")) * Mathf.Rad2Deg;
        myShootDir = Quaternion.AngleAxis(-myShotAngle, Vector3.forward) * Vector3.right;
        myRb = this.GetComponent<Rigidbody2D>();
        myRb.AddForce(myShootDir * mySpeed, ForceMode2D.Impulse);
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Enemy") || collision.gameObject.tag == ("Environment"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Enemy") || collision.gameObject.tag == ("Environment"))
        {
            collision.gameObject.SendMessage("TakeDamage", myDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
