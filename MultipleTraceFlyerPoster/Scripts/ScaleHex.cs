using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHex : MonoBehaviour
{

    public GameObject backGround;


    private Vector3 StartPosScale;
    private Vector3 EndPosScale;
    private Vector3 startPos;
    private Vector3 endPos;

    private Vector2 StartPosTile;
    private Vector2 EndPosTile;
    private Vector2 StartPosOffset;
    private Vector2 EndPosOffset;


    private float distance = 3f;
    private float distance2 = 0.05f;

    private float tiling = 3f;
    private float tileOffset = -1.5f;

    private float lerpTime = 0.5f;

    private float currentLerpTime;
    private float currentLerpTimeMore;

    private bool _isLerping = false;
    private bool _isStillLerping = false;

    private float Perc;
    private float PercMore;

    public IEnumerator StartLerping()
    {
        yield return new WaitForSeconds(0.5f);

        _isLerping = true;
        currentLerpTime = Time.time;

        startPos = backGround.transform.localPosition;
        endPos = endPos + Vector3.up * distance2;
    }

    // Start is called before the first frame update
    public IEnumerator ContinueLerping()
    {  yield return new WaitForSeconds(1f);
        
        currentLerpTimeMore = Time.time;
        _isStillLerping = true;

        StartPosScale = backGround.transform.localScale;
        EndPosScale = backGround.transform.localScale + Vector3.right * distance;

        StartPosTile = backGround.GetComponent<Renderer>().material.mainTextureScale;
        EndPosTile = backGround.GetComponent<Renderer>().material.mainTextureScale + Vector2.right * tiling;
        StartPosOffset = backGround.GetComponent<Renderer>().material.mainTextureOffset;
        EndPosOffset = backGround.GetComponent<Renderer>().material.mainTextureOffset + Vector2.right * tileOffset;


    }


    // Update is called once per frame
    void FixedUpdate()
    {   
        if (_isLerping == true){

                float timeSinceLerp = Time.time - currentLerpTime;
                float t = timeSinceLerp/lerpTime;
                PercMore = t*t*t * (t * (6f*t - 15f) + 10f);
                backGround.transform.localPosition = Vector3.Lerp(startPos, endPos, PercMore);

                if(t >= 1.0f)
                    {
                    _isLerping = false;
                    }
                
        }

        if (_isStillLerping == true){

                float timeSinceLerp = Time.time - currentLerpTimeMore;
                float t = timeSinceLerp/lerpTime;
                Perc = t*t*t * (t * (6f*t - 15f) + 10f);
                backGround.transform.localScale = Vector3.Lerp(StartPosScale, EndPosScale, Perc);
                backGround.GetComponent<Renderer>().material.mainTextureScale = Vector2.Lerp(StartPosTile, EndPosTile, Perc);
                backGround.GetComponent<Renderer>().material.mainTextureOffset = Vector2.Lerp(StartPosOffset, EndPosOffset, Perc);

                if(t >= 1.0f)
                    {
                    _isStillLerping = false;
                    }
                
        }

        
            
        
    }

    
    public void ReturnLerping()
    {  
        backGround.transform.localPosition = StartPosScale;
    }
    
}
