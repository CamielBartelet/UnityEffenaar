using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lefttoright : MonoBehaviour
{

public float speed = 0.8f;
Vector3 pointA;
Vector3 pointB;
float currentLerpTime;
private bool _isLerping;

void Start()
{
    pointA = new Vector3(0, 0, 0);
    pointB = new Vector3(5, 0, 0);
    
}

void GetItOn(){
    currentLerpTime = Time.time;
    _isLerping = true;
}

void FixedUpdate()
{   if(Input.GetKeyDown("space")){
        GetItOn();
    }
    //PingPong between 0 and 1
    if(_isLerping){
    float timeSinceLerp = Time.time - currentLerpTime;
    float t = timeSinceLerp/speed;
                        
    //float Perc = t*t*t * (t * (6f*t - 15f) + 10f);
    float time = Mathf.PingPong(t, 1);
    transform.position = Vector3.Lerp(pointA, pointB, time);

    if(t>=2.5f && transform.position == pointA)
            {
                _isLerping = false;
            }
        }
    

}

}
