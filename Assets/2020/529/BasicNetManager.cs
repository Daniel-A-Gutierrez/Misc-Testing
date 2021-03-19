using Mirror;
using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNetManager : NetworkManager
{
    int players = 0;
    public GameObject spawnLocations;
    //public override void OnStartServer()
    //{
        
    //    base.OnStartServer();
    //    //NetworkServer.RegisterHandler<CreateBaseMessage>(OnCreateBase);
    //}

    //public override void OnClientConnect(NetworkConnection conn)
    //{
    //    base.OnClientConnect(conn);//readies the client and sends add player message to server. 
    //    CreateBaseMessage message = new CreateBaseMessage { playerNumber = players };//theres probably a better way to do this.
    //    conn.Send(message);
    //}

    //void OnCreateBase(NetworkConnection nc, CreateBaseMessage message)
    //{
    //    if (nc.identity.gameObject == null )//this should never happen
    //    {
    //        print("This should never happen.");
    //        GameObject playerGameObject = Instantiate(playerPrefab);
    //        playerGameObject.GetComponent<NetBase>().playerNumber = players;
    //        NetworkServer.AddPlayerForConnection(nc, playerGameObject);
    //        players++;
    //    }
    //    else
    //    {
    //        nc.identity.gameObject.GetComponent<NetBase>().playerNumber = players;
    //        players++;
    //    }


    //}

    public override void OnServerAddPlayer(NetworkConnection nc)
    {
        //ClientScene.AddPlayer//called from client to make server call onserveraddplayer
        GameObject playerGameObject = Instantiate(playerPrefab, spawnLocations.transform.GetChild(players).transform.position, playerPrefab.transform.rotation);
        //playerGameObject.GetComponent<NetBase>().playerNumber = players;////////////
        Color c; switch (players)
        {
            case 0: c = Color.red; break;
            case 1: c = Color.white; break;
            case 2: c = Color.green; break;
            case 3: c = Color.magenta; break;
            default: c = Color.black; break;
        }
        NetworkServer.AddPlayerForConnection(nc, playerGameObject);
        //playerGameObject.GetComponent<NetColorer>().ChangeColor(c);///////////
        //playerGameObject.GetComponent<NetBase>().RpcRecolor(c);
        players++;players = players % 4;
    }

}
