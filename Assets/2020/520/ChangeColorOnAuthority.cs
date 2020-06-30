using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnAuthority : NetworkBehaviour
{

    public override void OnStartAuthority()//apparently this is a thing
    {
        base.OnStartAuthority();
        GetComponent<SpriteRenderer>().color = Color.blue;
        print("But");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
