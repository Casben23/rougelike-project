using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mySpeed = 50;
    public float myHealth = 200;

    private Rigidbody2D myRb;
    Animator myPlayerAnimator;

    Vector2 movement;

    void Start()
    {
        myRb = this.GetComponent<Rigidbody2D>();
        myPlayerAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY= Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY);

        movement.Normalize();

        if (moveX > 0 || Input.GetAxis("RightJoyX") > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveX < 0 || Input.GetAxis("RightJoyX") < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Mathf.Abs(moveX) > 0 || Mathf.Abs(moveY) > 0)
        {
            myPlayerAnimator.SetBool("Running", true);
        }
        else { myPlayerAnimator.SetBool("Running", false); }

        if(myHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        myRb.MovePosition(myRb.position + movement * mySpeed * Time.fixedDeltaTime);
    }
    
    public void TakeDamage(float aDmg)
    {
        myHealth -= aDmg;
    }

    public void Heal(float aHealAmount)
    {
        float temp = (myHealth) / aHealAmount;
        myHealth += temp;
    }

    public float GetHealth()
    {
        return myHealth;
    }
}
