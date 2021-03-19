using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class xqqz : NetworkBehaviour
{
    [SyncVar(hook = nameof(ChangeMyColor))]//this is cool
    Color col;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ChangeMyColor(Color Old, Color New)
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color", New);
    }


    [Command]
    void CmdColor(Color c)
    {
        col = c;
        GetComponent<MeshRenderer>().material.SetColor("_Color", col);
    }
    // Update is called once per frame
    void Update()
    {
        //GetComponent<MeshRenderer>().material.SetColor("_Color", col);
        if (!hasAuthority)
            return;
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            Color c = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            CmdColor(c);
        }
    }
}
