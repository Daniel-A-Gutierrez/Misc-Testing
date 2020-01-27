using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMan : MonoBehaviour
{
    public float height; 
    // x = v^2/ (2a)
    // 2a *x = v^2
    //v = (2ax)^.5
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * Mathf.Sqrt(2*9.81f*height),ForceMode2D.Impulse);
        }
    }
}
