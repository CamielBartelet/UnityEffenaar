using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using static Unity.Mathematics.math;

public class WaveCreator : MonoBehaviour
{
    public GameObject objectToStretch;
    public float spawnTime;
    public float spawnTime2 = 1;
    public float spawnDelay;

    public List<GameObject> objectList = new List<GameObject>();
    //public List<SquashAndStretchDeformer.SquashAndStretchJob> moveIt;
    public Vector3 newPos;
    private SquashAndStretchDeformer m_squashAndStretchDeformer;
    private SquashAndStretchDeformer.SquashAndStretchJob m_squashAndStretchJob;

    public Dictionary<string, float> objectDict = new Dictionary<string, float>();

    private bool _itLerps;
    private bool continueLerp = false;

    float startFactor = 0f;
    float endFactor = 200.0f;

    private float currentLerpTime;
    private float currentLerpTimeMore;
    private float Perc;

    private int t = 0;

    void Start()
    {
        m_squashAndStretchDeformer = new SquashAndStretchDeformer();
        InvokeRepeating("ObjectStretching", spawnTime, spawnDelay);
        objectToStretch.SetActive(false);


    }

    // Update is called once per frame
   

    void ObjectStretching() {
        
        GameObject clonedPillar = Instantiate(objectToStretch, newPos, transform.rotation);
        objectList.Add(clonedPillar);

        clonedPillar.name = objectToStretch.name + t;
        objectDict.Add(clonedPillar.name, startFactor);

        t++;


        Debug.Log(clonedPillar.name + "This is the end:"  + startFactor);


        newPos = clonedPillar.transform.position + Vector3.right;
        

        currentLerpTime = Time.time;

        clonedPillar.SetActive(true);

        
        _itLerps = true;

    }

     void FixedUpdate()
    {   MoveItUp();
        if(Input.GetKeyDown("space")){
            CancelInvoke();
        }

        if(objectList.Count == 8){
        
        CancelInvoke();
        }
    }

    public void MoveItUp(){
        if(_itLerps){
        for(int i = 0; i < objectList.Count; i++)
        {
            SquashAndStretchDeformer clonedScript = objectList[i].GetComponentInChildren<SquashAndStretchDeformer>();

                    if(clonedScript.Factor != endFactor){
                        //float GoingToStartFactor = startFactor;
                        //float GoingToEndFactor = endFactor;
                        float timeSinceLerp = Time.time - currentLerpTime;
                        float t = timeSinceLerp/spawnTime;
                        
                        Perc = t*t*t * (t * (6f*t - 15f) + 10f);

                        clonedScript.Factor = Mathf.Lerp(clonedScript.Factor, endFactor, Perc);
                        //clonedScript.Bottom = Mathf.Lerp(clonedScript.Bottom, 0, Perc);

                        // if(clonedScript.Factor == endFactor){

                        //     float p = Time.time/spawnTime;
                        //     float Percmore = t*t*t * (t * (6f*t - 15f) + 10f);
                        //     clonedScript.Factor = Mathf.Lerp(clonedScript.Factor, startFactor, Percmore);
                        //     _itLerps = false;
                        //     Debug.Log(clonedScript.Factor);
                        // }
                    }
                
                    

                    // if(t >= 1.0f)
                    //     {
                    //     _itLerps = false;
                    //     }
            // if(clonedScript.Factor == endFactor){
            //     objectList.Remove(objectList[i]);
            // }
                    
                
            
        }
        }

    }
}
