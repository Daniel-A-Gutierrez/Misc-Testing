using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank2 : MonoBehaviour
{
    public int playerNumber;
    public int maxHP;
    int hp;
    public KeyCode LEFT = KeyCode.A;
    public KeyCode FORWARD = KeyCode.W;
    public KeyCode BACKWARD = KeyCode.S;
    public KeyCode RIGHT = KeyCode.D;
    public KeyCode SHOOT = KeyCode.Space;

    public float moveSpeed;
    public float rotationSpeed;
    public float projectileVelocity;
    public GameObject projectile;
    Rigidbody rb;
    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = maxHP;
    }

    // Update is called once per frame
    float lastDr;
    float currentDr;
    void Update()
    {
        Vector3 movedir = (Input.GetKey(FORWARD) ? Vector3.forward : Vector3.zero)  + 
            (Input.GetKey(BACKWARD) ? Vector3.back : Vector3.zero) +
            (Input.GetKey(LEFT) ? Vector3.left : Vector3.zero) +
            (Input.GetKey(RIGHT) ? Vector3.right : Vector3.zero);
        movedir.Normalize();
        if(movedir!=Vector3.zero)
        {
            if(Vector3.Dot(transform.forward,movedir) > .9999f)
                rb.MovePosition(rb.position + movedir*moveSpeed*Time.deltaTime);
            else
            {
                //System.Numerics.Complex initial = new System.Numerics.Complex(transform.forward.z,transform.forward.x); 
                //System.Numerics.Complex goal = new System.Numerics.Complex(movedir.z,movedir.x);
                
                if(Vector3.Dot(transform.forward,movedir) > .999f)
                {
                    transform.forward = movedir;
                }
                else
                {
                    float targetRotation = Mathf.Atan2(movedir.z, movedir.x);
                    float currentRotation = Mathf.Atan2(transform.forward.z, transform.forward.x);
                    transform.Rotate(Vector3.down*rotationSpeed*Time.deltaTime * (dR(currentRotation,targetRotation) >0 ? 1 : -1) );
                }
            }
        }

        if(Input.GetKeyDown(SHOOT))
        {
            GameObject p = Instantiate(projectile,transform.position+transform.forward*2, Quaternion.identity);
            p.GetComponent<Rigidbody>().velocity = transform.forward * projectileVelocity;
            p.GetComponent<Bullet>().playernum = playerNumber;
        }

    }

    public void Damage(int amount)
    {
        hp-= amount;
        if(hp<=0)
            Die();
    }

    void Die()
    {
        gameObject.SetActive( false );
        Invoke("Respawn", 2);
        //Destroy(gameObject);//for now
    }

    void Respawn()
    {
        gameObject.SetActive( true );
        hp = maxHP;
        transform.position = spawnPosition;
    }

    //likes radians
    float dR(float rot1, float rot2) //still broken.
    {
        //from p1 to p2
        //path to the right ccw  : if p2 is less than p1, then ( pi - p1 ) + (p2 - -pi)
                                // if p2 is greater than p1, then p2-p1
        // path to the left cw : if p2 is less than p1 : (-pi - p2) + (pi - p1)
                                //if p2 is greater than p1 : (p1 - p2)
        //      -PI  p1     0       p2  PI
        if(rot2<rot1)
        {
            float ccw =  rot2 - rot1;// (Mathf.PI - rot1) + (rot2 + Mathf.PI);
            float cw = ( rot2+Mathf.PI) + (Mathf.PI- rot1);
            return (Mathf.Abs(cw) < Mathf.Abs(ccw) ? cw : ccw);
        }
        else if(rot2> rot1)// rot2 > rot1
        {
            float ccw = rot2 - rot1; 
            float cw = (-Mathf.PI - rot1) + (rot2- Mathf.PI);
            return (Mathf.Abs(cw) < Mathf.Abs(ccw) ? cw : ccw);
        }
        else
        {
            return 0;
        }
    }

    int FComp(float a, float b, float e = .0001f)
    {
        bool equal = (a <= b && (a + e) >= b);
        if(equal)
            return 0;
        else
            return (a>b) ? 1 : -1;
    }

    bool V3Comp(Vector3 a, Vector3 b, float e = .001f)
    {
        if(FComp(a.x,b.x,e) == 0 &&FComp(a.y,b.y,e) == 0 &&FComp(a.z,b.z,e) == 0 )
        {
            return true;
        }
        return false;
    }
}


