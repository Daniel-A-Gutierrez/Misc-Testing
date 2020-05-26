using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
    }
    float tpI = 1;
    float lastTPT = 0;
    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastTPT > tpI) 
        {
            lastTPT = Time.time;
            transform.position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
        }
    }
}
