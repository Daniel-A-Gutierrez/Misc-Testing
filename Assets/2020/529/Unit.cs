using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public Transform target;

    public void Go()
    {
        GetComponent<NavMeshAgent>()?.SetDestination(target.transform.position);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Go();
        }
    }
}
