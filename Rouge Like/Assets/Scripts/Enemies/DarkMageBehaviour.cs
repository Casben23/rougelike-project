using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMageBehaviour : MonoBehaviour
{
    public enum eStates
    {
        Following,
        Patrolling,
        Avoid,
        Attacking
    }

    private eStates myCurrentState;
    private Transform myPlayerPos;
    public GameObject myProjectile;
    private Rigidbody2D myRb;
    public float mySpeed;
    public float myPatrollSpeed;
    public float myStopDistans;
    public float myFollowDistans;
    private Vector3 myPatrollPosition;
    void Start()
    {
        myCurrentState = eStates.Following;
        myPlayerPos = GameObject.FindObjectOfType<PlayerController>().transform;
        myRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        myPlayerPos = GameObject.FindObjectOfType<PlayerController>().transform;
        switch (myCurrentState)
        {
            case eStates.Following:
                Following();
                break;
            case eStates.Patrolling:
                myPatrollPosition = GetRoamingPosition();
                Patrolling();
                break;
            case eStates.Avoid:
                Avoid();
                break;
            case eStates.Attacking:
                Attacking();
                break;
        }

        Debug.Log(myCurrentState);
    }

    void Patrolling()
    {
        if(Vector3.Distance(transform.position, myPlayerPos.position) > myFollowDistans)
        {
            myCurrentState = eStates.Following;
        }
        else if(Vector3.Distance(transform.position, myPlayerPos.position) < myStopDistans)
        {
            myCurrentState = eStates.Avoid;
        }
        else
        {
            if (transform.position != myPatrollPosition)
            {
                myRb.MovePosition(transform.position - myPatrollPosition * myPatrollSpeed * Time.fixedDeltaTime);
            }
            else
            {
                myPatrollPosition = GetRoamingPosition();
            }
        }
    }

    void Avoid()
    {
        Vector3 distance = transform.position - myPlayerPos.position;
        if(distance.magnitude >= myStopDistans)
        {
            myCurrentState = eStates.Patrolling;
        }
        else
        {
            myRb.MovePosition(distance.normalized * mySpeed * Time.fixedDeltaTime);
        }
    }

    void Following()
    {
        Vector3 movement = myPlayerPos.position - transform.position;
        myRb.MovePosition(-movement.normalized * mySpeed * Time.fixedDeltaTime);

        if(movement.magnitude <= myFollowDistans)
        {
            myCurrentState = eStates.Patrolling;
        }
    }

    void Attacking()
    {

    }

    Vector3 GetRoamingPosition()
    {
        Vector2 startPos = new Vector2(transform.position.x, transform.position.y);
        return startPos + Random.insideUnitCircle.normalized * Random.Range(10, 20);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myStopDistans);
        Gizmos.DrawWireSphere(transform.position, myFollowDistans);
    }
}
