using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int playernum;//player that shot this bullet.
    public int damage = 1;

    void OnCollisionEnter(Collision c)
    {
        Tank2 hit = c.gameObject.GetComponent<Tank2>();
        if(hit!= null)
        {
            if(hit.playerNumber!=playernum)
                hit.Damage(damage);
        }
        Terminate();
    }

    void Terminate()//could implement queueing later
    {
        Destroy(gameObject);
    }
}
