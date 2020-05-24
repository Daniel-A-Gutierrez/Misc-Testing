using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : MonoBehaviour
{
    public float speed = 1.0f;
    public float deleteAfter = 3.0f;
    float instantiatedAt = 0.0f;
    bool goout = false;

    private void Start()
    {
        instantiatedAt = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.fixedDeltaTime;
        if(Time.time < 8.0f)
            transform.RotateAround(transform.position, Vector3.forward, Mathf.Lerp(0,10, (Time.time-instantiatedAt)/4) );
        else if ( !goout)
        {
            goout = true;
            transform.up =  new Vector3(transform.position.x,transform.position.y,0).normalized;
        }

    }

    private void Update()
    {
        if (Time.time - deleteAfter > instantiatedAt)
            Destroy(gameObject);
    }
}
