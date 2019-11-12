using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class ScaleVideoTex : MonoBehaviour
{   

    public VideoClip[] vidsToPlayCapped;
    private OnTouchChildTrueFalse onTouchChildTrueFalse;
    private TouchFlyerAction touchFlyerAction;

    private GameObject arSesOr;
    
    public Transform leftObject;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 endPos2;

    private float distance = 2f;
    private float distance2 = 1f;

    private float lerpTime = 0.8f;

    private float currentLerpTime;

    private bool _isLerping;
    private float Perc;

    public VideoPlayer vp;

    void Awake() {
        vp = gameObject.GetComponent<VideoPlayer>();
        arSesOr = GameObject.Find("AR Session Origin");
        onTouchChildTrueFalse = arSesOr.GetComponent<OnTouchChildTrueFalse>();
        //touchFlyerAction = GetComponentInParent<TouchFlyerAction>();
        
        foreach (VideoClip vidcap in onTouchChildTrueFalse.vidsToPlay)
        {
            Debug.Log("Got 1");

        }
        
            //vidsToPlayCapped = touchFlyerAction.vidsToPlay;
        vidsToPlayCapped = onTouchChildTrueFalse.vidsToPlay;
    }

    public void StartLerping()
    {
        _isLerping = true;
        currentLerpTime = Time.time;
        
        startPos = leftObject.transform.localScale;
        endPos = leftObject.transform.localScale + Vector3.right * distance + Vector3.up * distance2;
        //endPos2 = endPos + Vector3.up * distance2;
    }

    // // Update is called once per frame
    // void Update()
    // {
    //   if(Input.GetKey(KeyCode.Space) && seeThis == false)
    //     {
            
    //         seeThis = true;
    //         StartLerping();
            
    //     }


    // }

    public void PlayVideo(int id, bool measure) {

        Debug.Log("GOT IT" + id);

        if(measure == true){
        vp.clip = vidsToPlayCapped[id];
        vp.Prepare();

        vp.Play();
        }

        else{
            vp.Stop();
        }
        
        
    }


    void FixedUpdate()
    {   if(_isLerping == true){

        float timeSinceLerp = Time.time - currentLerpTime - 0.5f;
        float t = timeSinceLerp/lerpTime;
        Perc = t*t*t * (t * (6f*t - 15f) + 10f);
        leftObject.transform.localScale = Vector3.Lerp(startPos, endPos, Perc);


        }
    }

    
    public void ReturnLerping()
    {  
        leftObject.transform.localScale = startPos;
    }
    
    
}
