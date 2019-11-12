using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class MultipleImageDetect : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private Button dismissButton;

    [SerializeField]
    private Text imageTrackedText;

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    // [SerializeField]
    // private Color activeColor = Color.blue;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f,0.1f,0.1f);

    [SerializeField]
    private bool IsSelected;

    public Text winText;
    
    Color[] defaultColor;

    private bool onTouchHold = false;
    // public bool Selected 
    // { 
    //     get 
    //     {
    //         return this.IsSelected;
    //     }
    //     set 
    //     {
    //         IsSelected = value;
    //     }
    // }

    private Vector2 touchPosition = default;

    private ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();
    private Dictionary<string, Color> arColor = new Dictionary<string, Color>();

    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss);
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        
        // setup all game objects in dictionary
        foreach(GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            arObjects.Add(arObject.name, newARObject);
            Color newColor = arObject.GetComponent<Renderer>().material.color;
            arColor.Add(arObject.name, newColor);
            
        }
        
        // defaultColor = new Color[arObjectsToPlace.Length];

        // for (int i = 0; i < arObjectsToPlace.Length; i++)
        //  {
        //      defaultColor[i] = arObjectsToPlace[i].GetComponent<Renderer>().material.color;
        //  }
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
            

            if(arObjectsToPlace != null)
            {
                GameObject goARObject = arObjects[name];
                goARObject.SetActive(true);
                goARObject.transform.position = newPosition;
                goARObject.transform.localScale = scaleFactor;
                Color colorSetting = arColor[name];
                
                LookTouch(goARObject, colorSetting);
                

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

    void LookTouch (GameObject touched, Color defaultColor){
            
        

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

    // void ChangeSelectedObject(PlacementObject selected)
    // {
    //     foreach (PlacementObject current in arObjectsToPlace)
    //     {   
    //         MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
    //         if(selected == current) 
    //         {
    //             current.Selected = true;
    //             meshRenderer.material.color = activeColor; 
    //         }
            
    //     }
    // }

}
