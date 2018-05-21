using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMine : MonoBehaviour {

    public float speed;
    public Rigidbody rb;

    private string up = "up";
    private string right = "right";
    private string down = "down";

    private bool activeTurn = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        setDirection(up);
    }

    void Update()
    {
        if(transform.position.z > 8 && transform.position.z < 9 && activeTurn)
        {
            setDirection(right);
            activeTurn = false;
        }
        if (transform.position.x > 4.20 && transform.position.x < 4.30)
        {
            setDirection(down);
        }
    }

    void setDirection(string direction)
    {
        if(direction == up)
        {
            rb.velocity = transform.forward * speed;
        }
        if (direction == right)
        {
            rb.velocity = transform.right * speed;
        }
        if (direction == down)
        {
            rb.velocity = transform.forward * -1 *speed;
        }
    }
    /*
    public float TimeTillTrack;
    public float CalculatedDistance;
    private float Timer;
    private GameObject FoundTargetObject;
    private Vector3 Target;
    public float Speed;
    public float LookSpeed;
    private Quaternion targetRotation;
    public float DistanceTillStopLooking;
    public float TimeTillNextTarget;
    public float stopLookingAfterTargetSwap;
    private bool stopTurning;

    void Start()
    {
        //Find the target object
        FoundTargetObject = GameObject.FindGameObjectWithTag("mine1");
        Target = FoundTargetObject.transform.position;
    }

    void Update()
    {
        //set up the timer
        Timer += Time.deltaTime;
        if (Timer > TimeTillNextTarget)
        {
            FoundTargetObject = GameObject.FindGameObjectWithTag("mine2");
            Target = FoundTargetObject.transform.position;
            stopTurning = false;
        }
        if (Timer > stopLookingAfterTargetSwap)
        {
            stopTurning = true;
        }
        //destroy if missile's time is up
        //find the distance from missile to target
        CalculatedDistance = Vector3.Distance(gameObject.transform.position, Target);
        //give the missile speed
        transform.Translate(0, 0, Speed / 100);
        //delay tracking for a certain amount of time...
        if (Timer > TimeTillTrack)
        {
            if (stopTurning == false)
            {
                //look at the target object at speed
                targetRotation = Quaternion.LookRotation(Target - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * LookSpeed);
            }
        }
        if (CalculatedDistance < DistanceTillStopLooking)
        {
            stopTurning = true;
            //Die = true;
        }
    }
    */
}
