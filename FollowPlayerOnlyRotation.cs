using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerOnlyRotation : MonoBehaviour {

    public float speedRotation;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        var Target = GameObject.FindGameObjectWithTag("Player").transform.position;
        var targetRotation = Quaternion.LookRotation(transform.position - Target);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedRotation);
    }
}
