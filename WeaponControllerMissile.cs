using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerMissile : MonoBehaviour {

    private AudioSource audioSource;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delayBetweenSeries;
    public float delay;
    public int amountOfMissiles;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("FireSerie", delay, delayBetweenSeries);
    }

    void FireSerie()
    {
        StartCoroutine(FireSerieEnum());
    }

    IEnumerator FireSerieEnum()
    {
        for(int i = 0; i < amountOfMissiles; i++)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
