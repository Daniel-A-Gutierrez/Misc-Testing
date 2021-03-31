using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//place this on the root object of an attack with a collider and a rigidbody, targeting an HP component.
/// <summary>
/// attack layer hits everything but collider and attack
/// collider hits everything but attack
/// hitbox hits only attack
/// </summary>
public class Attack : MonoBehaviour
{
    public GameObject parent;
    public int damage = 1;
    public float lifetime = 120;
    bool destroyOnHit = true;
    
    public void Start()
    {
        Invoke("Terminate", lifetime);
    }

    public void Terminate()
    {
        Destroy(gameObject);
    }

    public void Initialize(GameObject parent, int damage, bool destroyOnHit )
    {
        this.parent = parent;
        this.damage = damage;
        this.destroyOnHit = destroyOnHit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject!=parent &&collision.gameObject!= null)
        {
            HP hp = null;
            collision.gameObject.TryGetComponent<HP>(out hp);

            //debug
            print(collision.gameObject.name);

            if(hp!=null)
            {
                hp.TakeDamage(damage);
            }

            if(destroyOnHit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
