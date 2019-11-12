using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePillars : MonoBehaviour
{
    public GameObject go;
    private PillarUpdown pillarUpdown;
    public List<GameObject> objectList = new List<GameObject>();

    public float spawnTime;
    public float spawnDelay;
    public int pillarCount;
    private int pillarHalve;

    public Vector3 rightPos;
    public Vector3 leftPos;

    public float spaceInbetween = 1.1f;

    private int t = 0;
    private int p = 0;
    private int o;


    // Start is called before the first frame update
    void Start()
    {   pillarUpdown = new PillarUpdown();
        pillarHalve = pillarCount/2;
        o = pillarHalve;
        go.SetActive(false);
        for(int i = 0; i < pillarHalve; i++){
        GameObject clonedPillar = Instantiate(go, rightPos, transform.rotation);
        objectList.Add(clonedPillar);

        clonedPillar.name = go.name + t;

        t++;

        rightPos = clonedPillar.transform.position + Vector3.right * spaceInbetween;

        
            //objectList[i].SetActive(true);
        }

        // for(int y=0;y<cols;y++)
        // {
        //     for(int x=0;x<rows;x++)
        //     {
        //         Vector3 spawnPos = new Vector3 (Xstart+ x*(1+Xspace) , Ystart + y*(1+Yspace)  , 0);
        //         GameObject g = Instantiate(tile, spawnPos , Quaternion.identity)as GameObject;
        //         g.name = x + "/" + y; 
        //         g.transform.parent = gameObject.transform;
        //     }
        // }

        for(int i = pillarHalve; i < pillarCount; i++){
        GameObject clonedPillar = Instantiate(go, leftPos, transform.rotation);
        objectList.Add(clonedPillar);

        clonedPillar.name = go.name + t;

        t++;

        leftPos = clonedPillar.transform.position - Vector3.forward * spaceInbetween;

        
            //objectList[i].SetActive(true);
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
        objectList[p].SetActive(true);
        objectList[o].SetActive(true);
        objectList[p].GetComponent<PillarUpdown>().GetItOn();
        objectList[o].GetComponent<PillarUpdown>().GetItOn();
        p++;
        o++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            CallItin();
        }

        if(p == pillarHalve)
        {
            CancelInvoke();
            p=0;
            o=pillarHalve;
        }
    }

    void CallItin(){
        
            InvokeRepeating("ObjectStretching", spawnTime, spawnDelay);
        
    }
}
