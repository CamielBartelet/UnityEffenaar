using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// System.Serializable to make it show up in the inspector
[System.Serializable]
public class MyLinkedObjects
{
    public GameObject parentGameObject;
    //public Transform[] childObjects;
    public List<Transform> childObjects = new List<Transform>();
    

    public void RenameParentGameObject(string newNameForParentGameObject)
    {
        parentGameObject.name = newNameForParentGameObject;
    }

    // public void DisableFirstChildObject()
    // {
    //     linkedChildGameObject.SetActive(false);
    // }
}

public class SerializedClassScript : MonoBehaviour
{
    // Have a list of these objects show in the inspector and configure them there
    public List<MyLinkedObjects> myLinkedObjects;

    private void Start()
    {
        //myLinkedObjects[1].RenameParentGameObject("this is the new name for second parent gameobject");
        for(var i = 0; i < myLinkedObjects.Count; i++)
        {
            foreach (Transform child in myLinkedObjects[i].parentGameObject.transform)
            {
                myLinkedObjects[i].childObjects.Add(child.transform);
            }
        }
    }
}
