using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class JustTheTouch : MonoBehaviour
{

    MovingThrough movingThrough;
    public GameObject whichObject;

    public bool itsHit = false;

    [SerializeField]
    private Camera mainCamera;


    void Awake()
    {
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
                if(itsHit == false){
                if (Input.GetMouseButtonDown(0)){ // if left button pressed...
                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                        {
                                  
                                        StartCoroutine(movingThrough.StartLerping());
                                        StartCoroutine(movingThrough.ContinueLerping());
                                        itsHit = true;

                        }
                
            
                }
                }

        }



}
