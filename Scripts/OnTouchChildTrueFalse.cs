using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Video;

public class OnTouchChildTrueFalse : MonoBehaviour
{
    // Start is called before the first frame update
    private ARTrackedImageManager m_TrackedImageManager;

    private MovingTex movingTex;

    public Material[] movTex;

    [SerializeField]
    public GameObject[] prefabs;

    [SerializeField]
    public VideoClip[] vidsToPlay;

    private ScaleVideoTex[] scaleVideoTex;

    public bool onTouchHold = false;
    public bool onTouchAnim = false;

    public float OffsetX;
    public float OffsetY;

    public float ScrollX = 0.5f;
    public float ScrollY = 0.5f;

    private Vector2 touchPosition = default;

    private Vector2 realMove;

    [SerializeField]
    private Text imageTrackedText;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.5f,0.5f,0.5f);


    public Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();
    
    //public Dictionary<string, int> arObjectsChildren = new Dictionary<string, int>();
    public Dictionary<string, int> arVids = new Dictionary<string, int>();

    public Dictionary<string, Color> arColor = new Dictionary<string, Color>();

    public Dictionary<string, Material> arTexture = new Dictionary<string, Material>();

    
    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
         foreach (GameObject arObject in prefabs)
            //Instantiate Object. Called when complementing image.name is detected
                {   GameObject newArObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
                    newArObject.name = arObject.name;
                    newArObject.SetActive(false);
                    arObjects.Add(arObject.name, newArObject);
                    
                    foreach(Transform child in arObject.transform){
                        if(child.GetComponent<Renderer>().sharedMaterial.HasProperty("_Color")){
                        Color newColor = child.GetComponent<Renderer>().sharedMaterial.color;
                        arColor.Add(child.name, newColor);
                        }
                        

                        Material newTex = child.GetComponent<Renderer>().sharedMaterial;
                        arTexture.Add(child.name, newTex);
                                               
                        //arObjectsChildren.Add(arObject.name, child.name);
                        //arObjectsChildren[arObject.name].Add(child.name);
                    }
                }

                var t = 0;
                var k = -1;

            foreach (VideoClip vidplaced in vidsToPlay)
            {   //Give videoclip unique name with ID
                t = t + 1;
                k = k + 1;
                var vidID = k;
                var c = "Artist" + t;
                arVids.Add(c, vidID);
                //Debug.Log("Artist" + t);
            }

            foreach(KeyValuePair<string,int> attachStat in arVids)
            {
     //Now you can access the key and value both separately from this attachStat as:
            Debug.Log(attachStat.Key);
            Debug.Log(attachStat.Value);
            }
                
    }

    void Update(){
        float OffsetX = Time.time * ScrollX;
        float OffsetY = Time.time * ScrollY;
        realMove = new Vector2 (OffsetX, OffsetY);
    }

     void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            
            arObjects[trackedImage.name].SetActive(false);

        }
    }
    
    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Display the name of the tracked image in the canvas
       // imageTrackedText.text = trackedImage.referenceImage.name;

        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position, trackedImage.transform.rotation);

        Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
    }

    void AssignGameObject(string name, Vector3 newPosition, Quaternion newRotation)
    {
        
        if(arObjects != null){
            GameObject goARObject = arObjects[name];
            goARObject.SetActive(true);
            goARObject.transform.position = newPosition;
            goARObject.transform.localScale = scaleFactor;
            goARObject.transform.localRotation = newRotation;

            LookTouch(goARObject);

           if(goARObject != arObjects[name])
           {    
               
            //    foreach(TransFormTexLeft transitL in transFormTexLeft){ 
            //         transitL.ReturnLerping();
            //             }
            //         foreach(TransFormTexRight transitR in transFormTexRight){
            //         transitR.ReturnLerping();
            //             }
                        
            //         scaleVideoTex[0].ReturnLerping();
            //         scaleTex[0].ReturnLerping();
                    onTouchAnim = false;
                    goARObject.SetActive(false);
                    
           }
            
        }
    }

    void LookTouch (GameObject touched)
    { 

        if(touched.name == "FlyerNovember"){

            //Scripts must be combined
            TransFormTexLeft[] transFormTexLeft = touched.GetComponentsInChildren<TransFormTexLeft>();
            TransFormTexRight[] transFormTexRight = touched.GetComponentsInChildren<TransFormTexRight>();
            scaleVideoTex = touched.GetComponentsInChildren<ScaleVideoTex>();
            ScaleTex[] scaleTex = touched.GetComponentsInChildren<ScaleTex>();
            ScaleHex[] scaleHex = touched.GetComponentsInChildren<ScaleHex>();
            ScaleDate[] scaleDate = touched.GetComponentsInChildren<ScaleDate>();

            

            if(onTouchAnim == false){
                //After script combining less calls for Coroutine
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

                    scaleVideoTex[0].StartLerping();
                    StartCoroutine(scaleTex[0].StartLerping());
                    StartCoroutine(scaleHex[0].StartLerping());
                    StartCoroutine(scaleHex[0].ContinueLerping());
                    onTouchAnim = true;
                    }
        }

        Transform[] childObjects = touched.GetComponentsInChildren<Transform>();
        
        foreach(Transform child in childObjects){
        
        // for(int i = 0; i< childObjects.Length; i++){
        // arObjectsChildren.Add(child.name, i);
        // }
        //onTouchHold = false;

        if(Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                        
                    touchPosition = touch.position;

                        if(touch.phase == TouchPhase.Began)
                        {
                            Ray ray = arCamera.ScreenPointToRay(touch.position);
                            RaycastHit hitObject;
                            
                            if(Physics.Raycast(ray, out hitObject))
                            {
                                //scaleVideoTex[0].PlayVideo(arObjectsChildren[child.name]);
                                //touchFlyerAction[0].UpdateIt();
                                if(hitObject.transform == child && onTouchHold == false)
                                {
                                    onTouchHold = true;
                                    //ColorChange(child);
                                    //if(arVids.ContainsKey(child.name)){
                                    //scaleVideoTex[0].PlayVideo(arVids[child.name], true);
                                    //}
                                    SwitchVideo(child, touched);
                                    //TextureChange(child);


                                }
                                
                                else {
                                    if(hitObject.transform == child && onTouchHold == true){
                                            
                                            onTouchHold = true;
                                            //ColorChange(child);
                                            
                                            //scaleVideoTex[0].PlayVideo(arVids[child.name], false);
                                            SwitchVideo(child, touched);
                                            //TextureChange(child);

                                    }
                                }
                            }
                        }

                    // if(touch.phase == TouchPhase.Ended)
                    //     {
                    //         onTouchHold = false;
                    //     }
                }
        

                
        }
    }

    void ColorChange(Transform childTouched){
                
                //Optional
                Color defaultColor = arColor[childTouched.name];

                //childTouched.GetComponent<Renderer>().material.color = childTouched.GetComponent<Renderer>().material.GetColor("_Color");

                if (childTouched.GetComponent<Renderer>().material.color == defaultColor && onTouchHold == true)
                {
                    childTouched.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                    onTouchHold = true;
                    
                }
                else
                {
                    if(childTouched.GetComponent<Renderer>().material.color == Color.yellow && onTouchHold == true){

                        childTouched.GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
                        onTouchHold = false;
                        
                        
                    }
                }
    }

    void TextureChange(Transform childTouched)
    {           //Optional
                
                Material defaultMaterial = arTexture[childTouched.name];

                if (childTouched.GetComponent<Renderer>().sharedMaterial == defaultMaterial && onTouchHold == true)
                {   
                    childTouched.GetComponent<Renderer>().sharedMaterial = movTex[1];

                    childTouched.GetComponent<Renderer>().sharedMaterial.mainTextureOffset = realMove;
                    
                    onTouchHold = true;
                    
                }
                else
                {
                    if(childTouched.GetComponent<Renderer>().sharedMaterial != defaultMaterial && onTouchHold == true){
                        childTouched.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
                        
                        onTouchHold = false;
                        
                        
                    }
                }
    }

    void SwitchVideo(Transform childTouched, GameObject touched)
    {               
                   if(arVids.ContainsKey(childTouched.name)){
                    childTouched.GetComponent<Renderer>().sharedMaterial = movTex[0];
                    childTouched.GetComponent<Renderer>().sharedMaterial.mainTextureOffset = realMove;

                    scaleVideoTex[0].PlayVideo(arVids[childTouched.name], true);
                    onTouchHold = true;
                   }
                   else{
                       scaleVideoTex[0].vp.Stop();
                   }

                    foreach (Transform innactives in touched.transform)
                    {   Material defaultMaterial = arTexture[innactives.name];
                        if(arVids.ContainsKey(innactives.name)){
                            if(childTouched.name != innactives.name){
                                innactives.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
                            }
                        }
                    }
                    

    }


}
