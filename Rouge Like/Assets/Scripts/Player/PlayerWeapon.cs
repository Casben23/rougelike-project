using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    PlayerStatsManager myPlayerStatsManager;
    public float myTimeBtwShoots = 1000f;
    public GameObject myBullet;
    private float myShotAngle;
    Vector3 myShotDir;
    public Transform myFirePoint;
    private float timeBtwShoots;

    void Start()
    {
        myPlayerStatsManager = FindObjectOfType<PlayerController>().GetComponent<PlayerStatsManager>();
        timeBtwShoots = myTimeBtwShoots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RightJoyX") != 0 || Input.GetAxis("RightJoyY") != 0)
        {
            if (timeBtwShoots <= 0)
            {
                ShootWithController();
            }
        }

        if(Input.GetAxis("Fire1") != 0)
        {
            if(timeBtwShoots <= 0)
            {
                ShootWithMouse();
            }
        }

        timeBtwShoots -= myPlayerStatsManager.myAttackSpeed.GetValue() * Time.deltaTime;
    }

    void ShootWithController()
    {
        CalculateJoystickShotDirection();
        GameObject bullet = Instantiate(myBullet, myFirePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, -myShotAngle + 90)));
        timeBtwShoots = myTimeBtwShoots;
    }

    void ShootWithMouse()
    {
        CalculateMosueShotDirection();
        GameObject bullet = Instantiate(myBullet, myFirePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, myShotAngle)));
        timeBtwShoots = myTimeBtwShoots;
    }

    void CalculateJoystickShotDirection()
    {
        myShotAngle = Mathf.Atan2(Input.GetAxis("RightJoyY"), Input.GetAxis("RightJoyX")) * Mathf.Rad2Deg;
        myShotDir = Quaternion.AngleAxis(myShotAngle, Vector3.forward) * Vector3.right;
    }

    void CalculateMosueShotDirection()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mouseDir = (Input.mousePosition - playerPos).normalized;
        myShotAngle = Mathf.Atan2(mouseDir.x, -mouseDir.y) * Mathf.Rad2Deg;
        myShotDir = Quaternion.AngleAxis(myShotAngle, Vector3.forward) * Vector3.right;
    }
}
