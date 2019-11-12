using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDate : MonoBehaviour
{

    public GameObject dateandfooter;
    private Vector3 startPos;
    private Vector3 endPos;

    private float distance = 0.1f;

    private float lerpTime = 1;

    private float currentLerpTime;

    private bool _isLerping = false;

    private float Perc;
    
    
    public IEnumerator StartLerping()
    {   yield return new WaitForSeconds(0.5f);
        _isLerping = true;
        currentLerpTime = Time.time;

        startPos = dateandfooter.transform.localPosition;
        endPos = dateandfooter.transform.localPosition + Vector3.up * distance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isLerping == true){
            float timeSinceLerp = Time.time - currentLerpTime;
            float t = timeSinceLerp/lerpTime;
            Perc = t*t*t * (t * (6f*t - 15f) + 10f);
            dateandfooter.transform.localPosition = Vector3.Lerp(startPos, endPos, Perc);

            if(t >= 1.0f)
                {
                _isLerping = false;
                }
        }
            
        
    }

    
    public void ReturnLerping()
    {  
        dateandfooter.transform.localPosition = startPos;
    }
    
}
