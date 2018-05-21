using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapColors : MonoBehaviour {

    public float timeRate;
    public Color temporaryColor;

    private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        InvokeRepeating("swapColorFc", 0.1f, timeRate);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void swapColorFc()
    {
        StartCoroutine(swapColor());
    }

    IEnumerator swapColor()
    {
        meshRenderer.materials[1].SetColor("_Color", temporaryColor);
        yield return new WaitForSeconds(0.2f);
        meshRenderer.materials[1].SetColor("_Color", Color.white);
    }
}
