using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public new string tag;
    void OnCollisionEnter(Collision col)
    {
        if(tag == "" || tag == null)
            Destroy(gameObject);
        else if(col.gameObject.CompareTag(tag))
        {
            Destroy(gameObject);
        }
    }
}
