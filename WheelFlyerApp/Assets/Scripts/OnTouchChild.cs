using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class OnTouchChild : MonoBehaviour
{
    // Start is called before the first frame update
    private ARTrackedImageManager m_TrackedImageManager;

    

    [SerializeField]
    public GameObject[] prefabs;

    private bool onTouchHold = false;

    private Vector2 touchPosition = default;

    [SerializeField]
    private Text imageTrackedText;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.5f,0.5f,0.5f);

    public Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();
    
    //public Dictionary<string, string> arObjectsChildren = new Dictionary<string, string>();

    public Dictionary<string, Color> arColor = new Dictionary<string, Color>();

    
    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
         foreach (GameObject arObject in prefabs)
                {   GameObject newArObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
                    newArObject.name = arObject.name;
                    arObjects.Add(arObject.name, newArObject);
                    newArObject.SetActive(false);
                    
                    foreach(Transform child in arObject.transform){
                        Color newColor = child.GetComponent<Renderer>().sharedMaterial.color;
                        arColor.Add(child.name, newColor);
                        
                        //arObjectsChildren.Add(arObject.name, child.name);
                        //arObjectsChildren[arObject.name].Add(child.name);
                    }
                }
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
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);

        Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        
        if(arObjects != null){
            GameObject goARObject = arObjects[name];
            goARObject.SetActive(true);
            goARObject.transform.position = newPosition;
            goARObject.transform.localScale = scaleFactor;

            LookTouch(goARObject);

           if(goARObject != arObjects[name])
           {
               goARObject.SetActive(false);
           }
            
        }
    }

    void LookTouch (GameObject touched)
    {
        Transform[] childObjects = touched.GetComponentsInChildren<Transform>();
        
        foreach(Transform child in childObjects){
        
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
                                
                                if(hitObject.transform.GetComponent<Collider>() == child.GetComponent<Collider>() && onTouchHold == false)
                                {
                                    ColorChange(child);
                                    onTouchHold = true;
                                    
                                }
                                
                                else {
                                    if(hitObject.transform.GetComponent<Collider>() == child.GetComponent<Collider>() && onTouchHold == true){
                                
                                    
                                            ColorChange(child);
                                            onTouchHold = false;
                                        
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

                childTouched.GetComponent<Renderer>().material.color = childTouched.GetComponent<Renderer>().material.GetColor("_Color");

                if (onTouchHold == true)
                {
                    childTouched.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                }
                else
                {
                    childTouched.GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
                }
    }
}
