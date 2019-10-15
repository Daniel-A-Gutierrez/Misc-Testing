using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerController : MonoBehaviour
{
    Dictionary<string,Action> States;
    
    [SerializeField]
    string state;//for debugging, dont set

    public float walkSpeed;
    public float gravityScale;
    public bool gravityOn;
    public bool grounded;
    public float GroundCheckOffset;
    public float GroundCheckRadius;
    public LayerMask ground;

    float lastGravity;

    Rigidbody2D rb;
    Vector2 moveVec;

    void Awake()
    {
        States = new Dictionary<string,Action>();
        rb = GetComponent<Rigidbody2D>();
        state="DefaultState";
        States["DefaultState"] = DefaultState;
        lastGravity = gravityScale;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(moveVec!=Vector2.zero)
            rb.MovePosition(rb.position + moveVec);
    }

    void Update()
    {
        States[state]();
    }

    void EnterDefaultState()
    {
        state= "DefaultState";
        DefaultState();
    }

    void DefaultState()
    {
        SetMoveDir();
        CheckGrounded();
        ApplyGravity();
    }

    void ExitDefaultState()
    {
        moveVec = Vector2.zero;
    }


    //FUNCTIONS



    void ZeroMoveDir()
    {
        moveVec= Vector2.zero;
    }

    void SetMoveDir()
    {
        moveVec = ((Input.GetKey(KeyCode.A) ? Vector2.left : Vector2.zero) +
         (Input.GetKey(KeyCode.D) ? Vector2.right : Vector2.zero)).normalized *walkSpeed*Time.fixedDeltaTime;
    }

    void ApplyGravity() //rn gravity is linear
    {
        if(gravityScale == 0 || gravityOn ==false )
            print("Apply Gravity being called even though scale is " +
                gravityScale + " and gravity On is " + gravityOn);
        if(gravityOn && !grounded)
            moveVec += Vector2.down * gravityScale * Time.fixedDeltaTime;
    }

    void CheckGrounded()
    {
        Collider2D c = Physics2D.OverlapCircle(rb.position+ Vector2.up*GroundCheckOffset , GroundCheckRadius, ground);
        grounded = c == null ? false : true; 
    }

    void GravityOn()
    {
        gravityScale = lastGravity;
    }

    void GravityOff()
    {
        lastGravity = gravityScale;
        gravityScale = 0;
    }

    void SetGravity(float g)
    {
        if(g!=gravityScale)
        {
            lastGravity = gravityScale;
            gravityScale = g;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + Vector3.up*GroundCheckOffset , GroundCheckRadius);
    }


}
