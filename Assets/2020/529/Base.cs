using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public GameObject unit;
    public float radius;
    public int num;
    List<GameObject> units;
    public GameObject target;
    void Start()
    {
        units = new List<GameObject>();
    }

    //lets say each unit is a network instanced item and go from there. 
    void SpawnUnit()
    {
        float angle = Random.Range(0f, 6.28f);
        float dist = Random.Range(-radius, radius);
        units.Add(Instantiate(unit, transform.position +
            new Vector3(
            Mathf.Cos(angle) * dist,
            0,
            Mathf.Sin(angle) * dist)
            , Quaternion.identity));
        units[units.Count - 1].GetComponent<Unit>().target = target.transform;
    }

    int counter = 0;
    void Update()
    {
        
    }
}
