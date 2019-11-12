using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.ProBuilder;
using System.Collections.ObjectModel;

    public class ScaleSokkelLeft : MonoBehaviour
    {
        public GameObject sokkel;
        private Vector3 startPosLeft;
        private Vector3 endPosLeft;
        private Vector3 startPosLeftContinue;
        private Vector3 endPosLeftContinue;

        private TouchFlyerAction touchFlyerAction;
        //private float extrusion;

        private float distance = 0.4f;
        private float distance2 = 0.2f;
        private float distance3 = -1.5f;
        private float distance4 = -0.1f;

        private float lerpTime = 1f;
        private float lerpTime2 = 1f;

        private float currentLerpTime;
        private float currentLerpTimeMore;

        private bool _isLerping;
        private bool _isStillLerping;
        private float Perc;
        private float PercMore;

        // Start is called before the first frame update

        //public Dictionary<string, GameObject> arSokkels = new Dictionary<string, GameObject>();

        private GameObject[] arSokkels;

        void Start(){
            touchFlyerAction = GetComponent<TouchFlyerAction>();

            //sokkel.SetActive(false);

            for(int i = 0; i < 4; i++){
            float t = i/7;
            Vector3 sokkelposition = sokkel.transform.position + Vector3.right * t;
            GameObject clonedSokkel = Instantiate(sokkel, sokkelposition, sokkel.transform.rotation);
            clonedSokkel.name = sokkel.name + i;

            }

            // foreach(GameObject sokkeltje in arSokkels)
            // {
            //     sokkeltje.SetActive(true);
            // }

            //for(int i = 0; i < 8; i++){
                
                //moreSokkel.transform.position = sokkel.transform.position + Vector3.right * i;
            //}
            //extrusion = sokkel.GetComponent<PolyShape>().extrude;
           //Debug.Log(extrusion);

        }
        
        public IEnumerator StartLerping()
        {   yield return new WaitForSeconds(0.5f);
            _isLerping = true;
            currentLerpTime = Time.time;
            
            startPosLeft = sokkel.transform.localScale;
            endPosLeft = sokkel.transform.localScale + Vector3.forward * distance;

            //endPos2 = endPosRight + Vector3.up * distance2;
        }

        public IEnumerator ContinueLerping()
        {
            yield return new WaitForSeconds(1f);

            _isStillLerping = true;
            currentLerpTimeMore = Time.time;

            startPosLeftContinue = sokkel.transform.localPosition;
            endPosLeftContinue = sokkel.transform.localPosition + Vector3.right * distance3 + Vector3.forward * distance4;
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
            sokkel.transform.localScale = Vector3.Lerp(startPosLeft, endPosLeft, Perc);

            if(t >= 1.0f)
                {
                    _isLerping = false;
                }
            }

            if(_isStillLerping == true){

            float timeSinceLerp = Time.time - currentLerpTimeMore;
            float t = timeSinceLerp/lerpTime2;
            PercMore = t*t*t * (t * (6f*t - 15f) + 10f);
            sokkel.transform.localPosition = Vector3.Lerp(startPosLeftContinue, endPosLeftContinue, PercMore);

            

            if(t >= 1.0f)
                {
                    _isStillLerping = false;
                }
            }
        }

        public void ReturnLerping()
        {  
            sokkel.transform.localPosition = startPosLeftContinue;
        }

    }
