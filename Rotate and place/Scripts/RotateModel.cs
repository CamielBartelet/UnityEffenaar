using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class RotateModel : MonoBehaviour
{
    public Slider slider;
    //public GameObject model;
    public ARTrackedImageManager m_TrackedImageManager;
    GameObject m_TrackedImagePrefab;
    
    void Awake()
    {
        GameObject managerScript = GameObject.Find("AR Session Origin");
        m_TrackedImageManager = managerScript.GetComponent<ARTrackedImageManager>();
        //m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    public void SliderRotatingModel()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, slider.value, transform.rotation.z);
    }
//     void UpdateInfo(ARTrackedImage trackedImage)
// {

//   if (trackedImage.trackingState != TrackingState.Tracking)
//     {

//      if(trackedImage.referenceImage.name == "Effenaar"){
//         Instantiate(Cube, transform.position, transform.rotation);
//       }

//     }else{
//     // Destroy object if you dont want to keep
//    }
// }

    
}
