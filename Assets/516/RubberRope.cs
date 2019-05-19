using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberRope : MonoBehaviour
{
    public GameObject node;
    public int number;
    public Vector3 gap;//y must be 0
    void Start()
    {
        if(number>=1){
            GameObject pre = Instantiate(node,transform.position,Quaternion.identity,gameObject.transform);
            GameObject post = null;

            for(int i  = 1; i < number; i++)
            {
                post = Instantiate(node,transform.position + gap*i,Quaternion.identity,gameObject.transform);
                post.GetComponent<WaveButton>().connections.Add(pre);
                pre.GetComponent<WaveButton>().connections.Add(post);
                pre = post;
            }
        }
    }

    void Update()
    {

    }

}
