using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using static Unity.Mathematics.math;

public class PillarUpdown : MonoBehaviour
{

    public float speed = 0.5f;
    float pointA;
    public float pointB;
    float currentLerpTime;
    private bool _isLerping;

    SquashAndStretchDeformer clonedScript;

    void Start()
    {
        pointA = 0;
        pointB = 400;

        clonedScript = GetComponentInChildren<SquashAndStretchDeformer>();
        
    }

    public void GetItOn(){
        currentLerpTime = Time.time;
        _isLerping = true;
    }

    void FixedUpdate()
    {   //if(Input.GetKeyDown("space")){
        //   GetItOn();
        //}
        //PingPong between 0 and 1
        if(_isLerping){
        float timeSinceLerp = Time.time - currentLerpTime;
        float t = timeSinceLerp/speed;
                            
        //float Perc = t*t*t * (t * (6f*t - 15f) + 10f);
        float time = Mathf.PingPong(t, 1);
        clonedScript.Factor = Mathf.Lerp(pointA, pointB, time*time*time * (time * (6f*time - 15f) + 10f));

        if(t>=2)
                {
                    clonedScript.Factor = pointA;
                    _isLerping = false;
                }
            }
        

    }

}
