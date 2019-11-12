using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransFormTexRight : MonoBehaviour
{
    public GameObject rightObject;
    private Vector3 startPosRight;
    private Vector3 endPosRight;
    private Vector3 startPosLeftContine;
    private Vector3 endPosLeftContinue;

    private TouchFlyerAction touchFlyerAction;
    
    //private float distance = 0.5f;
    private float distance2 = 0.2f;
    private float distance3 = 1.5f;
    private float distance4 = 0.1f;

    private float lerpTime = 1f;
    private float lerpTime2 = 1f;

    private float currentLerpTime;
    private float currentLerpTimeMore;

    private bool _isLerping;
    private bool _isStillLerping;
    private float Perc;
    private float PercMore;

    // Start is called before the first frame update

    void Start(){
        touchFlyerAction = GetComponent<TouchFlyerAction>();
    }
    
    public IEnumerator StartLerping()
    {   yield return new WaitForSeconds(0.5f);
        _isLerping = true;
        currentLerpTime = Time.time;
        
        startPosRight = rightObject.transform.localPosition;
        endPosRight = rightObject.transform.localPosition + Vector3.up * distance2;
        //endPos2 = endPosRight + Vector3.up * distance2;
    }

    public IEnumerator ContinueLerping()
    {
        yield return new WaitForSeconds(1f);

        _isStillLerping = true;
        currentLerpTimeMore = Time.time;

        startPosLeftContine = endPosRight;
        endPosLeftContinue = endPosRight + Vector3.right * distance3 + Vector3.forward * distance4;
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     bool uGetIt = touchFlyerAction.giveIt;
    //   if(uGetIt == true && seeThis == false)
    //     {   
    //         seeThis = true;
    //         StartLerping();
    //     }
    // }

    void FixedUpdate()
    {          
        if(_isLerping == true){

        float timeSinceLerp = Time.time - currentLerpTime;
        float t = timeSinceLerp/lerpTime;
        Perc = t*t*t * (t * (6f*t - 15f) + 10f);
        rightObject.transform.localPosition = Vector3.Lerp(startPosRight, endPosRight, Perc);

        if(t >= 1.0f)
            {
                _isLerping = false;
            }
        }

        if(_isStillLerping == true){

        float timeSinceLerp = Time.time - currentLerpTimeMore;
        float t = timeSinceLerp/lerpTime2;
        PercMore = t*t*t * (t * (6f*t - 15f) + 10f);
        rightObject.transform.localPosition = Vector3.Lerp(startPosLeftContine, endPosLeftContinue, PercMore);

        

        if(t >= 1.0f)
            {
                _isStillLerping = false;
            }
        }
    }

    public void ReturnLerping()
    {  
        rightObject.transform.localPosition = startPosRight;
    }

}
