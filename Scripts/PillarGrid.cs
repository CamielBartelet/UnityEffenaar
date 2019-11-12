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

    public int cols;
    public int rows;
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
    {   go.SetActive(false);
        for(int y=0;y<cols;y++)
        {
            for(int x=0;x<rows;x++)
            {   
                Vector3 spawnPos = new Vector3 (Xstart+ x*(1+Xspace) , 0, -(Ystart + y*(1+Yspace)));
                GameObject going = Instantiate(go, spawnPos , go.transform.rotation) as GameObject;
                going.name = x + "/" + y; 
                //going.transform.parent = gameObject.transform;
                
                if(x%2==1)
                {
                    going.transform.position = going.transform.position - Vector3.forward * distance;
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
                        movingArtist.transform.position = movingArtist.transform.position + Vector3.forward * curPosition.y * 0.1f;
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
