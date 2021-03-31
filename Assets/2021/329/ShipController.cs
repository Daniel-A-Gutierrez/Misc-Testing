using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    /// <summary>
    /// shoot key lmb fire both
    /// wasd move
    /// ship faces towards mouse cursor
    /// space initiates dash in current movement direction, if any movement button is pressed
    /// </summary>
    
    public float speed;
    public float firerate;
    public GameObject bullet;

    Camera cam ;
    Vector3 movedir;
    Rigidbody rb;
    Transform rightgun;
    Transform leftgun;
    float lastfiretime;
    float fireperiod;

    void Start()
    {
        cam = Camera.main;
        movedir = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        rightgun = transform.Find("rightgun");
        leftgun = transform.Find("leftgun");
        lastfiretime = 0;
        fireperiod = 1f/firerate;
    }

    // Update is called once per frame
    void Update()
    {        
        transform.forward = (GetMouseWorldPosition() - transform.position).normalized;
        movedir = GetInputMoveDir();
        fireperiod = 1f/firerate;
        HandleShooting();
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movedir * speed * Time.deltaTime);
    }

    //utility functions

    //get mouse position on xz plane
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePix = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePix);
        Plane p = new Plane(Vector3.up, 0);
        float dist = 0;
        p.Raycast(ray, out dist);
        Vector3 worldpoint = ray.origin + ray.direction * dist;
        return worldpoint;
    }

    Vector3 GetInputMoveDir()
    {
        //fetch input
        Vector3 movedir = new Vector3(
            Input.GetAxis("Horizontal"), 
            0,
            Input.GetAxis("Vertical"));
        if(movedir.sqrMagnitude>.01)
            movedir.Normalize();
        else
            return Vector3.zero;

        //rotate to match camera facing
        Vector3 camoffset = transform.position - cam.transform.position ;
        camoffset.y = 0;
        camoffset.Normalize();
        Quaternion q = Quaternion.LookRotation(camoffset, Vector3.up);

        return q*movedir;

    }

    void HandleShooting()
    {
        if(Input.GetMouseButton(0) && Time.time - fireperiod > lastfiretime)
        {
            GameObject b1 = Instantiate(bullet, leftgun.position, Quaternion.identity );
            GameObject b2 = Instantiate(bullet, rightgun.position, Quaternion.identity );
            b1.GetComponent<Attack>().Initialize(gameObject,1,true);
            b2.GetComponent<Attack>().Initialize(gameObject,1,true);
            b1.GetComponent<GoStraight>().direction = transform.forward;
            b2.GetComponent<GoStraight>().direction = transform.forward;
            lastfiretime = Time.time;
        }
    }
}
