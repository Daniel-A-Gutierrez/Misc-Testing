using Mirror;
using Mirror.Examples.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SyncListUnit : SyncList<NetworkIdentity> { }
public class NetBase : NetworkBehaviour
{
    public GameObject unit;
    public float radius;
    public int num;
    SyncListUnit units;
    readonly SyncListUnit my_units = new SyncListUnit();
    public Vector3 target;
    [SyncVar]
    public int playerNumber=-1;
    void Start()
    {
        target = new Vector3(0, 0, 10);
    }

    //lets say each unit is a network instanced item and go from there. 
    [Command]
    void CmdSpawnUnit()
    {
        float angle = UnityEngine.Random.Range(0f, 6.28f);
        float dist = UnityEngine.Random.Range(-radius, radius);
        GameObject newUnit = Instantiate(unit, transform.position +
            new Vector3(Mathf.Cos(angle) * dist,0,Mathf.Sin(angle) * dist), Quaternion.identity);
        newUnit.name = "Unit " + my_units.Count;
        newUnit.GetComponent<NetUnit>().target = target; //do i have to do an on serialize / deserialize for this?
        newUnit.GetComponent<NetColorer>().ChangeColor( GetComponent<NetColorer>().color);
        NetworkServer.Spawn(newUnit, this.connectionToClient);
        my_units.Add(newUnit.GetComponent<NetworkIdentity>());

        /*units.Add(Instantiate(unit, transform.position +
            new Vector3(
            Mathf.Cos(angle) * dist,
            0,
            Mathf.Sin(angle) * dist)
            , Quaternion.identity));*/
        //units[units.Count - 1].GetComponent<Unit>().target = target.transform;
    }

    int counter = 0;
    void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.S))
        { 
            CmdSpawnUnit();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CmdRecolor(Color.red);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            CmdRecolor(Color.white);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            CmdRecolor(Color.green);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            CmdRecolor(Color.magenta);

        //print(my_units.Count);

    }

   [Command]
    public void CmdRecolor(Color c)
    {
        RpcRecolorUnits(c);
        GetComponent<NetColorer>().ChangeColor(c);
        RpcRecolor(c);
    }

    [ClientRpc]
    public void RpcRecolor(Color c)
    {
        GetComponent<NetColorer>().ChangeColor(c);
    }
   
    [ClientRpc]
    void RpcRecolorUnits(Color c)
    {
        foreach (var Unit in my_units)
        {
            Unit.GetComponent<NetColorer>().ChangeColor(c);
        }
    }
    

    //spawning a unit : Client base decides it wants to, requests server to establish networked unit, gives authority to player.
    //so server must do the spawning. then we need a sync list for the units. 

}
