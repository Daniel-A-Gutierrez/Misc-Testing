using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoEvent : MonoBehaviour
{
    public GameEvent keysDown;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            keysDown.Raise();
        }        
    }
}
