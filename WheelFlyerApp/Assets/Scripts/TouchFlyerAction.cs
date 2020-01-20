using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TouchFlyerAction : MonoBehaviour
{
    private TransFormTexLeft[] transFormTexLeft;
    private TransFormTexRight[] transFormTexRight;
    private ScaleVideoTex[] scaleVideoTex;
    private ScaleTex[] scaleTex;
    private ScaleHex[] scaleHex;
    private ScaleDate[] scaleDate;
    private ScaleSokkelLeft[] scaleSokkelLeft;
    MovingThrough movingThrough;
    public GameObject whichObject;

    [SerializeField]
    public VideoClip[] vidsToPlay;

    private GameObject clickedObject;
        
    [SerializeField]
    private Camera mainCamera;

    public bool gettingIt;
    public bool giveIt;


    void Awake()
    {
        transFormTexLeft = GetComponentsInChildren<TransFormTexLeft>();
        transFormTexRight = GetComponentsInChildren<TransFormTexRight>();
        scaleVideoTex = GetComponentsInChildren<ScaleVideoTex>();
        scaleTex = GetComponentsInChildren<ScaleTex>();
        scaleHex = GetComponentsInChildren<ScaleHex>();
        scaleDate = GetComponentsInChildren<ScaleDate>();
        scaleSokkelLeft = GetComponentsInChildren<ScaleSokkelLeft>();
        movingThrough = whichObject.GetComponent<MovingThrough>();
        
    }

        // Update is called once per frame
        void Update()
        {
            // if(Input.touchCount > 0)
            //     {
            //         Touch touch = Input.GetTouch(0);
                        
            //         touchPosition = touch.position;

            //             if(touch.phase == TouchPhase.Began)
            //             {
            //                     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //                     RaycastHit hit;
            //                     if (Physics.Raycast(ray, out hit)){
                if (Input.GetMouseButtonDown(0)){ // if left button pressed...
                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                        {
                                    //Debug.Log("Hello: ");
                                    //if(hit.transform == cube){
                                        foreach(TransFormTexLeft transitL in transFormTexLeft){
                                        
                                        StartCoroutine(transitL.StartLerping());
                                        StartCoroutine(transitL.ContinueLerping());
                                        }
                                        foreach(TransFormTexRight transitR in transFormTexRight){
                                        
                                        StartCoroutine(transitR.StartLerping());
                                        StartCoroutine(transitR.ContinueLerping());
                                        }

                                        foreach(ScaleDate scaler in scaleDate){
                                            StartCoroutine(scaler.StartLerping());
                                        }

                                        foreach(ScaleSokkelLeft scaleSokkel in scaleSokkelLeft){
                                            StartCoroutine(scaleSokkel.StartLerping());
                                            StartCoroutine(scaleSokkel.ContinueLerping());
                                        }


                                        scaleVideoTex[0].StartLerping();
                                        StartCoroutine(scaleTex[0].StartLerping());
                                        StartCoroutine(movingThrough.StartLerping());
                                        StartCoroutine(scaleHex[0].StartLerping());
                                        StartCoroutine(scaleHex[0].ContinueLerping());
                                        

                        }
                
            
                }

                if (Input.GetKeyDown("space")){
                    int t = 0;
                    scaleVideoTex[0].PlayVideo(t, true);
                }
        }



}
