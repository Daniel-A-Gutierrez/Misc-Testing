using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAndShoot : MonoBehaviour
{
    public GameObject Bullet;
    GameObject familiar;
    float degreesToRotate;
    public float RPS;
    // Start is called before the first frame update
    void Start()
    {
        familiar = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            degreesToRotate += RPS * 360 * Time.fixedDeltaTime;
            int degreesRotated = 0;
            while (degreesToRotate > degreesRotated + 1)
            {
                familiar.transform.RotateAround(transform.position, Vector3.forward, 1);  //RPS*360*Time.fixedDeltaTime
                degreesRotated++;
                if (Mathf.Floor(familiar.transform.eulerAngles.z) %3 == 0)
                {
                    Instantiate(Bullet, familiar.transform.position, familiar.transform.rotation);
                }
                degreesRotated++;
            }
            degreesToRotate -= degreesRotated;
        }
    }
}
/*
 ok so thats done but i still have like no idea how Id program in patterns well.
 lets say a pattern consists of a series of tuples of (bullet, position, rotation, time)
     
     */