using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveButton : MonoBehaviour
{
    public List<GameObject> connections;
    public float SpringConstant;
    Rigidbody rb;
    public bool special = false;
    Vector3 InitialPosition;
    public float pressForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject i in connections)
        {
            rb.AddForce(Vector3.up*CalculateForce(i),ForceMode.Force);
        } 
        rb.AddForce(Vector3.up* (SpringConstant * (InitialPosition.y-transform.position.y )));
        if(Input.GetKeyDown(KeyCode.Space) && special)
        {
            Press();
        }
    }

    float CalculateForce(GameObject go)
    {
        return SpringConstant * (go.transform.position.y-transform.position.y);
    }

    public void Press()
    {
        rb.AddForce(Vector3.down*pressForce,ForceMode.VelocityChange);
    }
}
