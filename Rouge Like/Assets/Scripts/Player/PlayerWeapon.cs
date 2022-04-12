using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    PlayerStatsManager myPlayerStatsManager;
    public float myTimeBtwShoots = 5f;
    public GameObject myBullet;
    private float myShotAngle;
    Vector3 myShotDir;
    public Transform myFirePoint;
    private float timeBtwShoots;
    public Transform myChargePivot;

    public Image myChargeImage;

    [SerializeField]
    private float myChargeValue;
    [SerializeField]
    bool myIsCharging;

    public float myMaxChargeValue;
    
    void Start()
    {
        myChargeImage.enabled = false;
        myIsCharging = false;
        myChargeValue = 0;
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

        if(Input.GetAxis("Fire1") != 0 && timeBtwShoots <= 0)
        {
            myIsCharging = true;
            ChargeShot();
        }

        if(myIsCharging && Input.GetAxis("Fire1") == 0)
        {
            myIsCharging = false;
            ShootWithMouse();
        }

        if (myIsCharging)
        {
            myChargeImage.enabled = true;
            myChargeImage.fillAmount = myChargeValue * 0.01f;
        }
        else
        {
            myChargeImage.enabled = false;
        }

        myChargeImage.transform.position = myChargePivot.position;
        float finalAttackSpeed = 0.1f * (myPlayerStatsManager.myAttackSpeed.GetValue() * 0.3f) * Time.deltaTime;
        timeBtwShoots -= finalAttackSpeed * 0.3f;
    }

    public bool IsCharging()
    {
        return myIsCharging;
    }

    void ShootWithController()
    {
        CalculateJoystickShotDirection();
        GameObject bullet = Instantiate(myBullet, myFirePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, -myShotAngle + 90)));
        bullet.GetComponent<PlayerProjectile>().SetBullet(myChargeValue);
        timeBtwShoots = myTimeBtwShoots;
        AudioManager.Instance.PlaySound(AudioManager.Sound.PlayerAttack, transform.position, false, false);
    }
    void ShootWithMouse()
    {
        myIsCharging = false;
        CalculateMouseShotDirection();
        GameObject bullet = Instantiate(myBullet, myFirePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, myShotAngle)));
        bullet.GetComponent<PlayerProjectile>().SetBullet(myChargeValue);
        myChargeValue = 0;
        timeBtwShoots = myTimeBtwShoots;
        AudioManager.Instance.PlaySound(AudioManager.Sound.PlayerAttack, transform.position, false, false);
    }

    void ChargeShot()
    {
        myChargeValue += 5 * myPlayerStatsManager.myAttackSpeed.GetValue() * Time.deltaTime;
        if(myChargeValue >= myMaxChargeValue)
        {
            ShootWithMouse();
        }
    }


    void CalculateJoystickShotDirection()
    {
        myShotAngle = Mathf.Atan2(Input.GetAxis("RightJoyY"), Input.GetAxis("RightJoyX")) * Mathf.Rad2Deg;
        myShotDir = Quaternion.AngleAxis(myShotAngle, Vector3.forward) * Vector3.right;
    }

    void CalculateMouseShotDirection()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mouseDir = (Input.mousePosition - playerPos).normalized;
        myShotAngle = Mathf.Atan2(mouseDir.x, -mouseDir.y) * Mathf.Rad2Deg;
        myShotDir = Quaternion.AngleAxis(myShotAngle, Vector3.forward) * Vector3.right;
    }
}
