using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircle : MonoBehaviour {

    float angle = 0;
    float speed2 = (2 * Mathf.PI) / 5;//2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius = 5;
    void Update()
    {
        angle += speed2 * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        transform.position = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius + 5.5f);
    }
}
