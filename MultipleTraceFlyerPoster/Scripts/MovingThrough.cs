using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThrough : MonoBehaviour
{
    private Vector3 startPosLeftContinue;
    private Vector3 endPosLeftContinue;
    private Vector3 centerItIs;

    public GameObject backGround;
    private Vector3 startPos;
    private Vector3 endPos;

    private float distance = 0.59f;
    private Vector3 distance2 = new Vector3(2f, 0, 0);

    private float lerpTime = 1;
    private float lerpTime2 = 1f;

    private bool _mouseState;
	public Vector3 screenSpace;
	public Vector3 offset;

    private float currentLerpTime;
    private float currentLerpTimeMore;

    private bool _isLerping = false;
    private bool _isStillLerping = false;

    private float Perc;
    private float PercMore;

    public Dictionary<GameObject, Vector3> children = new Dictionary<GameObject, Vector3>();
    
    
    public IEnumerator StartLerping()
    {   yield return new WaitForSeconds(0.5f);
        _isLerping = true;
        currentLerpTime = Time.time;

        startPos = backGround.transform.localPosition;
        endPos = backGround.transform.localPosition + Vector3.up * distance;
    }

    public IEnumerator ContinueLerping()
    {
        yield return new WaitForSeconds(2f);
        centerItIs = new Vector3(0, -17f, 0.2f);

        
        _isStillLerping = true;
        
        currentLerpTimeMore = Time.time;
        //foreach(Transform child in backGround.transform){
            for(int i = 0; i < backGround.transform.childCount; i++){
                startPosLeftContinue = backGround.transform.GetChild(i).transform.localPosition;
                endPosLeftContinue = startPosLeftContinue + distance2;
                children.Add(backGround.transform.GetChild(i).gameObject, endPosLeftContinue);
                }

    }

    // void OnMouseDrag()
    //     {
    //         Debug.Log("Dragging");
    //     Vector3 cursorScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    //     float cursorPosition = Input.mousePosition.y;
    //     //transform.position = cursorPosition;
    //     foreach (Transform child in backGround.transform)
    //     {
    //     child.transform.RotateAround(centerItIs, Vector3.right, cursorPosition);
    //     }
    //     }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isLerping == true){
            float timeSinceLerp = Time.time - currentLerpTime;
            float t = timeSinceLerp/lerpTime;
            Perc = t*t*t * (t * (6f*t - 15f) + 10f);
            backGround.transform.localPosition = Vector3.Lerp(startPos, endPos, Perc);


            if(t >= 1.0f)
                {
                _isLerping = false;
                }
        }

        if(_isStillLerping == true){
        
        // float timeSinceLerp = Time.time - currentLerpTimeMore;
        // float t = timeSinceLerp/lerpTime2;
        // PercMore = t*t*t * (t * (6f*t - 15f) + 10f);

        // foreach(Transform child in backGround.transform){
        // child.transform.localPosition = Vector3.Lerp(child.transform.localPosition, children[child.gameObject], PercMore);
        
        // }

        // if (Input.GetMouseButtonDown(0)){ // if left button pressed...
        //             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //             RaycastHit hit;
        //             if (Physics.Raycast(ray, out hit))
        //                 {
        //                      Debug.Log("Dragging");
        //                      foreach (Transform child in backGround.transform)
        //                      {
        //                          child.transform.RotateAround(centerItIs, Vector3.right, offset * Input.mousePosition.y);
        //                      }
        //                 }
        // }

        // Debug.Log(_mouseState);
		if (Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo)) {
				_mouseState = true;
				screenSpace = Camera.main.WorldToScreenPoint (backGround.transform.position);
				offset = backGround.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			_mouseState = false;
		}
		if (_mouseState) {
			//keep track of the mouse position
			var curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

			//convert the screen mouse position to world point and adjust with offset
			var curPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;

			//update the position of the object in the world
            foreach (Transform child in backGround.transform)
                {
                    child.transform.RotateAround(centerItIs, Vector3.right, 0.5f* curPosition.y);
                }
        }
        

        // if(t >= 1.0f)
        //     {
        //         _isStillLerping = false;
        //     }
        }
            
        
    }
    
    public void ReturnLerping()
    {  
        backGround.transform.localPosition = startPos;
    }
    
}
