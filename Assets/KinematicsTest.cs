using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicsTest : MonoBehaviour
{
    public float maxHeight = 0f;
    public float timeAtStart;
    public float timeAtPeak;
    public float timeAtEclipse;
    bool printed= false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(Vector3.up*5,ForceMode.Impulse);
        timeAtStart = Time.time;
    }


    void FixedUpdate()
    {
        if(transform.position.y>maxHeight)
        {
            maxHeight = transform.position.y;
            timeAtPeak = Time.time;
        }
        if(transform.position.y>0)
        {
            timeAtEclipse = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>5 && !printed)
        {
            printed = true;
            print(timeAtStart);
            print(timeAtPeak);
            print(timeAtEclipse);
            print(maxHeight);
        }
    }
}
