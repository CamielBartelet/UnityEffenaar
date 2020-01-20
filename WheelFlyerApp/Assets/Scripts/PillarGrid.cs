using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGrid : MonoBehaviour
{
    public GameObject go;
    public GameObject movingArtist;
    private PillarUpdown pillarUpdown;
    public List<GameObject> objectList = new List<GameObject>();

    public float spawnTime;
    public float spawnDelay;

    [SerializeField]
    private Camera mainCamera;

    private float mainDegree;
    private float k;
    private Vector3 centerItIs;


    public int rows;
    public int cols;
    // spacing variable 
    public float Xspace;
    public float Yspace;
    //point where the first tile is made
    private float Xstart;
    private float Ystart;
    public float distance = 0.5f;

    private bool _mouseState;
	public Vector3 screenSpace;
	public Vector3 offset;

    private int p = 0;


    // Start is called before the first frame update
    void Start()
    {   centerItIs = new Vector3(10.3f, -47.3f, 0);
        go.SetActive(false);
        for(int y=0;y<rows;y++)
        {
            for(int x=0;x<cols;x++)
            {   
                Vector3 spawnPos = new Vector3 (Xstart+ x*(1+Xspace) , 0, -(Ystart + y*(1+Yspace)));
                GameObject going = Instantiate(go, spawnPos , go.transform.rotation) as GameObject;
                going.name = x + "/" + y; 
                //going.transform.parent = gameObject.transform;
                mainDegree = 35.08f/(rows*2);
                k = (rows/2)-y;

                

                if(x%2==1)
                {   
                    
                    
                        going.transform.position = going.transform.position - Vector3.forward * distance;
                        
                        float n = k - distance + 0.25f;
                        float d = -0.024f*(n * n) + 0.002f*n + 1.14f;
                                                    
                                
                        float degree = mainDegree * n;
                        going.transform.Rotate(degree, 0, degree);
                        going.transform.position = going.transform.position + Vector3.up * d;
                    }
                    else {
                            
                            float c = -0.024f*(k * k) + 0.002f*k + 1.14f;
                            float degreetwo = mainDegree * k;
                            going.transform.Rotate(degreetwo, 0, degreetwo);
                            going.transform.position = going.transform.position + Vector3.up * c;
                        }
                        
                


                    

                objectList.Add(going);
                

            }
        }

    }

    void ObjectStretching(){
        // for(int i = 0; i < objectList.Count; i++)
        // {
        //     if(i == pillarCount){
        //         pillarUpdown.GetItOn();
        //         pillarCount++
        //     }
        // }
        
        //Invullen: if (p is in cols[i]){set active}

        objectList[p].SetActive(true);
        objectList[p].GetComponent<PillarUpdown>().GetItOn();
        p++;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            CallItin();
        }

        if(p == cols*rows)
        {
            CancelInvoke();
            p=0;
        }
        
            // if (Input.GetMouseButtonDown(0)){ // if left button pressed...

            //     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //     RaycastHit hit;

            //     if (Physics.Raycast(ray, out hit)){
            //             Debug.Log(hit.transform.name);
                        
            //             hit.transform.GetComponent<PillarUpdown>().GetItOn();
                        
            //         }
            // }

            if (Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo)) {
				_mouseState = true;
                
				screenSpace = Camera.main.WorldToScreenPoint (movingArtist.transform.position);
                
                offset = movingArtist.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
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
                //foreach (GameObject pillartje in objectList)
                //    {
                        //pillartje.transform.RotateAround(centerItIs, Vector3.right, 0.5f* curPosition.y);
                        //movingArtist.transform.position = movingArtist.transform.position + Vector3.forward * curPosition.y * 0.1f;
                        movingArtist.transform.RotateAround(centerItIs, Vector3.right, 0.5f* curPosition.y);
                //    }
            }

            for(int i = 0; i < objectList.Count; i++){
                if(movingArtist.GetComponent<Collider>().bounds.Contains(objectList[i].transform.position))
                {
                    objectList[i].transform.GetComponent<PillarUpdown>().GetItOn();
                }

                
            }
        
        
    }

    void CallItin(){
        
            InvokeRepeating("ObjectStretching", spawnTime, spawnDelay);
        
    }
}
