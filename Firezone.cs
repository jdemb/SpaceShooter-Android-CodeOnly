﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Firezone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

  
    private bool touched;
    private int pointerID;
    private bool canFire;

    void Awake()
    {
        
        touched = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        //set our start point
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            canFire = true;
        }
    }
 
    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            canFire = false;
            touched = false;
        }
    }

   public bool CanFire()
    {
        return canFire;
    }
}
