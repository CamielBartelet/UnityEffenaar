﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTex : MonoBehaviour
{
  public Renderer rend;

  public OnTouchChildTrueFalse touchCheck;
    
    // Update is called once per frame
    public float ScrollX = 0.5f;
    public float ScrollY = 0.5f;

    void Start() {

      rend = GetComponent<Renderer>();
      rend.enabled = true;
      
    }

    void Update(){
        float OffsetX = Time.time * ScrollX;
        float OffsetY = Time.time * ScrollY;
        rend.sharedMaterial.mainTextureOffset = new Vector2 (OffsetX, OffsetY);
        
    }

    
}
