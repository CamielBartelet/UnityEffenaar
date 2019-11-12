using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using NestedDictionaryLib;
using System.Linq;

[System.Serializable]
public class ARObjectsToPlace
{
    public GameObject parentGameObject;
    //public Transform[] childObjects;

    public List<Transform> childObjects = new List<Transform>();
    

    // public void RenameParentGameObject(string newNameForParentGameObject)
    // {
    //     parentGameObject.name = newNameForParentGameObject;
    // }

    // public void EnableParentGameObject(){

    //         parentGameObject.SetActive(true);
    // }
    
    // public void DisableParentGameObject()
    // {
    //     arObjects.SetActive(false);
    // }
}


[RequireComponent(typeof(ARTrackedImageManager))]
public class MultipleImageAndChildren : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private Button dismissButton;

    [SerializeField]
    private Text imageTrackedText;

    [SerializeField]
    public List<ARObjectsToPlace> arObjectsToPlace;

    // [SerializeField]
    // public GameObject[] arObjectPrefabs;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f,0.1f,0.1f);

    private bool onTouchHold = false;

    private Vector2 touchPosition = default;

    private ARTrackedImageManager m_TrackedImageManager;

    public Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();
    private Dictionary<string, Color> arColor = new Dictionary<string, Color>();

    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss);
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        //myLinkedObjects[1].RenameParentGameObject("this is the new name for second parent gameobject");
        foreach (ARObjectsToPlace arObject in arObjectsToPlace)
                {   GameObject newArObject = Instantiate(arObject.parentGameObject, Vector3.zero, Quaternion.identity);
                    newArObject.name = arObject.parentGameObject.name;
                    arObjects.Add(arObject.parentGameObject.name, newArObject);

                    foreach (Transform child in arObject.parentGameObject.transform)
                    {   
                        arObject.childObjects.Add(child.transform);

                        Color newColor = child.GetComponent<Renderer>().material.color;
                        arColor.Add(child.name, newColor);
                    }
                    
                }
        
        // for(var i = 0; i < arObjectsToPlace.Count; i++)
        //     {
        //         foreach (Transform childColor in arObjectsToPlace[i].parentGameObject.transform)
        //             {
        //                 Color newColor = childColor.GetComponent<Renderer>().sharedMaterial.color;
        //                 arColor.Add(childColor.name, newColor);
        //             }
        //     }

    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void Dismiss() => welcomePanel.SetActive(false);

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
        imageTrackedText.text = trackedImage.referenceImage.name;

        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);

        Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
            if(arObjects != null)
            {    
                    GameObject goARObject = arObjects[name];
                    goARObject.SetActive(true);
                    //goARObject.childObjects.SetActive(true);
                    goARObject.transform.position = newPosition;
                    goARObject.transform.localScale = scaleFactor;

                    // Transform[] allChildren = goARObject.GetComponentsInChildren<Transform>();
                    // foreach(Transform child in allChildren)
                    // {
                    
                    // Color colorSetting = arColor[child.name];

                    // LookTouch(child, colorSetting);
                    // }

                foreach(GameObject go in arObjects.Values)
                {
                    Debug.Log($"Go in arObjects.Values: {go.name}");
                    if(go.name != name)
                    {
                        go.SetActive(false);
                    }
                } 
                
                
                
            }
       
    }

    void LookTouch (Transform touched, Color defaultColor)
    {
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
                                
                                
                                if(hitObject.transform.GetComponent<Collider>() == touched.GetComponent<Collider>() && onTouchHold == false)
                                {
                                    onTouchHold = true;
                                    
                                }
                                
                                else {
                                    if(hitObject.transform.GetComponent<Collider>() == touched.GetComponent<Collider>() && onTouchHold == true){
                                
                                    
                                    
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

                touched.GetComponent<Renderer>().material.color = touched.GetComponent<Renderer>().material.GetColor("_Color");

                if (onTouchHold == true)
                {
                    touched.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                }
                else
                {
                    touched.GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
                }
    }
}
