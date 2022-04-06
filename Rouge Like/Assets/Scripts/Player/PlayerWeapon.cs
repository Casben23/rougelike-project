using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float myTimeBtwShoots = 0.8f;
    public GameObject myBullet;
    private float myShotAngle;
    Vector3 myShotDir;
    public Transform myFirePoint;
    private float timeBtwShoots;

    //STATS!
    [SerializeField]
    float myDamage;
    [SerializeField]
    float myAttackSpeed;
    [SerializeField]
    float myBulletSpeed;

    void Start()
    {
        timeBtwShoots = myTimeBtwShoots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RightJoyX") != 0 || Input.GetAxis("RightJoyY") != 0)
        {
            if (timeBtwShoots <= 0)
            {
                Shoot();
            }
        }

        timeBtwShoots -= Time.deltaTime;
    }

    void Shoot()
    {
        CalculateShotDirection();
        GameObject bullet = Instantiate(myBullet, myFirePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, -myShotAngle + 90)));
        bullet.GetComponent<PlayerProjectile>().SetBullet(myDamage, myBulletSpeed);
        timeBtwShoots = myTimeBtwShoots;
    }

    void CalculateShotDirection()
    {
        myShotAngle = Mathf.Atan2(Input.GetAxis("RightJoyY"), Input.GetAxis("RightJoyX")) * Mathf.Rad2Deg;
        myShotDir = Quaternion.AngleAxis(myShotAngle, Vector3.forward) * Vector3.right;
    }
}
