using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChange : MonoBehaviour
{   
    public GameObject cube;
    public Material[] material;
    Renderer rend;
    
    [SerializeField]
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.Find("Cube");
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ // if left button pressed...
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                //if(hit.transform == cube){
                    rend.sharedMaterial = material[1];
                //}
            }
            else
        {
            rend.sharedMaterial = material[0];
        }
        }
        
    }
}
