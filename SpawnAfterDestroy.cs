using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAfterDestroy : MonoBehaviour {

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;
    public Transform pos6;
    public Transform pos7;
    public Transform pos8;
    public GameObject mine;

    public void destroy()
    {
        Instantiate(mine, pos1.position, pos1.rotation);
        Instantiate(mine, pos2.position, pos2.rotation);
        Instantiate(mine, pos3.position, pos3.rotation);
        Instantiate(mine, pos4.position, pos4.rotation);
        Instantiate(mine, pos5.position, pos5.rotation);
        Instantiate(mine, pos6.position, pos6.rotation);
        Instantiate(mine, pos7.position, pos7.rotation);
        Instantiate(mine, pos8.position, pos8.rotation);
    }
}
