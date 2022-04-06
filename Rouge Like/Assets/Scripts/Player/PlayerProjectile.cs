using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    PlayerStatsManager myPlayerStatsManager;
    Rigidbody2D myRb;
    private float myShotAngle;
    Vector3 myShootDir;
    
    private void Awake()
    {
        myPlayerStatsManager = FindObjectOfType<PlayerController>().GetComponent<PlayerStatsManager>();
        myRb = this.GetComponent<Rigidbody2D>();
        SetBullet();
    }

    public void SetBullet()
    {
        if (Input.GetKey(KeyCode.Mouse0)) { SetShootAngleWithMouse(); }
        if (Input.GetAxis("RightJoyY") != 0 || Input.GetAxis("RightJoyX") != 0) { SetShootAngleWithJoystick(); }

        Shoot();
    }

    void SetShootAngleWithMouse()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mouseDir = (Input.mousePosition - playerPos).normalized;
        myShotAngle = Mathf.Atan2(mouseDir.x, mouseDir.y) * Mathf.Rad2Deg;
        myShootDir = Quaternion.AngleAxis(-myShotAngle + 90, Vector3.forward) * Vector3.right;
    }
    void SetShootAngleWithJoystick()
    {
        myShotAngle = Mathf.Atan2(Input.GetAxis("RightJoyY"), Input.GetAxis("RightJoyX")) * Mathf.Rad2Deg;
        myShootDir = Quaternion.AngleAxis(-myShotAngle, Vector3.forward) * Vector3.right;
    }

    void Shoot()
    {
        myRb.AddForce(myShootDir * myPlayerStatsManager.myProjectileSpeed.GetValue(), ForceMode2D.Impulse);
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            collision.GetComponent<EnemyHealth>()?.TakeDamage(myPlayerStatsManager.myDamage.GetValue());
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == ("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
