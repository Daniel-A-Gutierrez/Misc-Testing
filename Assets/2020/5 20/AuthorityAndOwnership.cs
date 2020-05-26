using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorityAndOwnership : NetworkBehaviour
{
    /*
     If chess were a networked game, authority is what one player has over their own pieces, but not
     the other player's. In a networked game the server has authority over everything, and players
     are considered owners of specific objects, and can ask the server to manipulate them on their behalf. 

     Authority allows a player to make commands to a server, via remote call. 

     The network manager confers authority to a player over an object
     when the player requests to spawn said object from a prefab. 

    The following are ways to gain authority. 
     */

    private GameObject _somePrefab;
    void Start()
    {
        bool playerDead = false;
        if(playerDead && base.hasAuthority && base.isClient)
        {
            CmdRequestRespawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RequestOwnershipOnMouse();
        }
    }


    [Command]//tag a function as a command to be run on the server
    private void CmdRequestRespawn()
    {
        GameObject result = Instantiate(_somePrefab);
        //does authority handling stuff. assigns result to whatever is in connectionToClient, which is the client requesting the command . 
        NetworkServer.Spawn(result, base.connectionToClient);
    }

    private void RequestOwnershipOnMouse()
    {
        if (!base.hasAuthority)
            return;//technically unnecessary but saves cycles
        RaycastHit hit;
        print("Thiing~");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            NetworkIdentity id = hit.collider.GetComponent<NetworkIdentity>();
            if(id!= null && !
                id.hasAuthority)
            {
                Debug.Log("Sending request authority for " + hit.collider.gameObject.name);
                CmdRequestAuthority(id);
            }
        }
    }

    [Command]
    private void CmdRequestAuthority(NetworkIdentity otherId)
    {
        Debug.Log("Recieved request athority for  " + otherId.gameObject.name);
        otherId.AssignClientAuthority(base.connectionToClient);
    }
}
