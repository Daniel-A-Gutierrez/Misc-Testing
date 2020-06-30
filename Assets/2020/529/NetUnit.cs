using Mirror;
using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NetUnit : NetworkBehaviour
{
    [SyncVar]
    public Vector3 target;

    [Command]
    public void CmdGo()
    {
        GetComponent<NavMeshAgent>()?.SetDestination(target); //would i normally have to send an rpc here?
    }




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && base.hasAuthority)
        {
            CmdGo();
        }
    }
}
