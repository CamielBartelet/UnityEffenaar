using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Video;

public class MultipleTouchWorking : MonoBehaviour
{
    // Start is called before the first frame update
    private ARTrackedImageManager m_TrackedImageManager;

    private MovingTex movingTex;

    public Material[] movTex;

    [SerializeField]
    public GameObject[] prefabs;

    [SerializeField]
    public VideoClip[] vidsToPlay;

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
    
    public Dictionary<string, int> arObjectsChildren = new Dictionary<string, int>();

    public Dictionary<string, Color> arColor = new Dictionary<string, Color>();

    public Dictionary<string, Material> arTexture = new Dictionary<string, Material>();

    
    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
         foreach (GameObject arObject in prefabs)
                {   GameObject newArObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
                    newArObject.name = arObject.name;
                    newArObject.SetActive(false);
                    arObjects.Add(arObject.name, newArObject);
                    
                    foreach(Transform child in arObject.transform){
                        // Vector3 newPos = child.position;
                        // arPos.Add(child.name, newPos);
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

            // TransFormTexLeft[] transFormTexLeft = goARObject.GetComponentsInChildren<TransFormTexLeft>();
            // TransFormTexRight[] transFormTexRight = goARObject.GetComponentsInChildren<TransFormTexRight>();
            // ScaleVideoTex[] scaleVideoTex = goARObject.GetComponentsInChildren<ScaleVideoTex>();
            // ScaleTex[] scaleTex = goARObject.GetComponentsInChildren<ScaleTex>();

            // if(onTouchAnim == false){
            //     foreach(TransFormTexLeft transitL in transFormTexLeft){ 
            //         transitL.StartLerping();
            //             }
            //         foreach(TransFormTexRight transitR in transFormTexRight){
            //         transitR.StartLerping();
            //             }

            //         scaleVideoTex[0].StartLerping();
            //         scaleTex[0].StartLerping();
            //         onTouchAnim = true;
            //         }

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
    {   //ScaleVideoTex[] selectionMade = touched.GetComponentsInChildren<ScaleVideoTex>();
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
                                //selectionMade[0].PlayVideo(arObjectsChildren[child.name]);
                                //touchFlyerAction[0].UpdateIt();
                                if(hitObject.transform == child && onTouchHold == false)
                                {
                                    onTouchHold = true;
                                    //ColorChange(child);
                                    //selectionMade[0].PlayVideo(0);
                                    TextureChange(child);


                                }
                                
                                else {
                                    if(hitObject.transform == child && onTouchHold == true){
                                            
                                            onTouchHold = true;
                                            //ColorChange(child);
                                            //selectionMade[0].PlayVideo(0);
                                            TextureChange(child);

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
    {
                
                Material defaultMaterial = arTexture[childTouched.name];

                if (childTouched.GetComponent<Renderer>().sharedMaterial == defaultMaterial && onTouchHold == true)
                {   
                    childTouched.GetComponent<Renderer>().sharedMaterial = movTex[1];

                    childTouched.GetComponent<Renderer>().sharedMaterial.mainTextureOffset = realMove;
                    
                    onTouchHold = true;
                    
                }
                else
                {
                    if(childTouched.GetComponent<Renderer>().sharedMaterial == movTex[1] && onTouchHold == true){
                        childTouched.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
                        
                        onTouchHold = false;
                        
                        
                    }
                }
    }


}
