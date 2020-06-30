using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBoi : NetworkBehaviour
{
    public float speed;
    Vector3 movedir;
    [SyncVar]
    public string myfavorite = "banana";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movedir = Vector3.zero;
        print(myfavorite);

        if (!this.isLocalPlayer)
            return;
        movedir = ( (Input.GetKey(KeyCode.A) ? Vector3.left : Vector3.zero) +
            (Input.GetKey(KeyCode.D) ? Vector3.right : Vector3.zero) +
            (Input.GetKey(KeyCode.W) ? Vector3.forward : Vector3.zero ) +
            (Input.GetKey(KeyCode.S) ? Vector3.back : Vector3.zero)).normalized;
        if (Input.GetKey(KeyCode.Space))
            CmdFavorite("Apples");
        if (Input.GetKey(KeyCode.Z))
            NotCmdFavorite("Tarantulas");
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(movedir * speed);
    }

    [Command]
    void CmdMoveATinyBit()
    {
        transform.position += Random.Range(.5f, 1.5f) * Vector3.up;
    }
    [Command]
    void CmdFavorite(string fav)
    {
        myfavorite = fav; 
    }

    void NotCmdFavorite(string cfav)
    {
        myfavorite = cfav;
    }
}
