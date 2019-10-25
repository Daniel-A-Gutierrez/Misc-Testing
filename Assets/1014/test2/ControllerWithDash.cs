using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ControllerWithDash : MonoBehaviour
{
        Dictionary<string,Action> States;
    
    [SerializeField]
    string state;//for debugging, dont set

    public float walkSpeed;
    public float gravityScale;
    public bool gravityOn;
    public bool grounded;
    public float GroundCheckOffset;
    public Vector2 GroundCheckBounds;
    public LayerMask ground;

    public float RoofCheckOffset;
    public Vector2 RoofCheckBounds;
    public LayerMask roof;
    [SerializeField]
    float currentGravity;
    [Range(.01f,1f)]
    public float jumpFloatiness;

    public float jumpHeight;
    public bool useHeight;
    public float jumpVelocity;
    public float jumpCooldown;
    public float airControl;
    public float fallSpeedCap;
    float lastJumpTime;
    public float maxAirSpeed;
    
    Collider2D[] colliders;
    Rigidbody2D rb;
    Vector2 moveVec; //changed by frame
    public AnimationCurve fallSpeed;

    public bool roofed;

    [Range(1f,10f)]
    public float sprintMultiplier;

    [Range(.1f,100f)]
    public float dashDistance;
    [Range(.01f,60f)]
    public float dashTime;

    void Awake()
    {
        States = new Dictionary<string,Action>();
        rb = GetComponent<Rigidbody2D>();
        state="DefaultState";
        States["DefaultState"] = DefaultState;
        States["JumpState"]  = JumpState;
        States["FallingState"] = FallingState;
        States["SprintingState"] = SprintingState;
        currentGravity = gravityScale;
        colliders = new Collider2D[8];
    }

    void Start()
    {
        
    }

    void FixedUpdate()//set max allowable timestep to 1/60, same as normal fixed timestep.
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

    //moveVec, grounded
    void DefaultState()
    {
        SetMoveDir();
        CheckGrounded();
        CheckRoofed();
        ApplyGravity();


        if(Input.GetKeyDown(KeyCode.Space) & jumpCooldown < (Time.time - lastJumpTime) & !roofed & grounded)
        {
            ExitDefaultState();
            EnterJumpState();
        }
        else if(!grounded)
        {
            ExitDefaultState();
            EnterFallingState();
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            ExitDefaultState();
            EnterSprintingState();
        }
    }

    void ExitDefaultState()
    {
        //moveVec = Vector2.zero;
    }

    bool stillHoldingSpace;
    void EnterJumpState() //should I segregate between jumping and falling? I think so.
    {
        grounded = false;
        stillHoldingSpace = Input.GetKey(KeyCode.Space);
        state = "JumpState";
        lastJumpTime = Time.time;
        moveVec.y = jumpVelocity * Time.fixedDeltaTime;
        if(useHeight)
            moveVec.y = Mathf.Sqrt(2f * gravityScale * jumpHeight * Time.fixedDeltaTime);
        JumpState();
    }

    void JumpState()
    {
        if(Input.GetKeyUp(KeyCode.Space))
            stillHoldingSpace = false;
        SetGravity(stillHoldingSpace ? gravityScale*jumpFloatiness : gravityScale);
        if(Time.time - lastJumpTime>jumpCooldown)
            CheckGrounded();
        SetMoveDirAir();
        ApplyGravity();
        CheckRoofed();
        if(moveVec.y <= 0 || roofed)
        {
            ExitJumpState();
            EnterFallingState();
        }
        if(grounded)
        {
            ExitJumpState();
            EnterDefaultState();
        }
        
    }

    void ExitJumpState()
    {
        SetGravity(gravityScale);
    }

    void EnterFallingState()
    {
        SetGravity(gravityScale);
        moveVec.y = 0;
        state = "FallingState";
    }

    void FallingState()
    {
        CheckGrounded();
        SetMoveDirAir();
        ApplyGravity();
        if(grounded)
        {
            ExitFallingState();
            EnterDefaultState();
        }

    }

    void ExitFallingState()
    {

    }

    void EnterSprintingState()
    {
        state = "SprintingState";
    }

    void SprintingState()
    {
        SetMoveDir();
        moveVec.x *= sprintMultiplier;
        CheckGrounded();
        CheckRoofed();
        ApplyGravity();


        if(Input.GetKeyDown(KeyCode.Space) & jumpCooldown < (Time.time - lastJumpTime) & !roofed & grounded)
        {
            ExitSprintingState();
            EnterJumpState();
        }
        else if(!grounded)
        {
            ExitSprintingState();
            EnterFallingState();
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            ExitSprintingState();
            EnterDefaultState();
        }
    }

    void ExitSprintingState()
    {

    }
    //FUNCTIONS


    //moveVec
    void ZeroMoveDir()
    {
        moveVec= Vector2.zero;
    }

    //moveVec
    void SetMoveDir()
    {
        moveVec = ((Input.GetKey(KeyCode.A) ? Vector2.left : Vector2.zero) +
         (Input.GetKey(KeyCode.D) ? Vector2.right : Vector2.zero)).normalized *walkSpeed*Time.fixedDeltaTime;
    }

    //applies input to moveVec without resetting Y. takes air control into account. 
    void SetMoveDirAir()
    {
        print("0");
        float apply = ((Input.GetKey(KeyCode.A) ? -1 : 0) +
            (Input.GetKey(KeyCode.D) ? 1 : 0))*walkSpeed*Time.fixedDeltaTime*airControl;
        if(Mathf.Abs(moveVec.x) < Mathf.Abs(maxAirSpeed * Time.fixedDeltaTime))
            {moveVec.x += apply; print("1");}
        else if(moveVec.x >= maxAirSpeed*Time.fixedDeltaTime) //allow decelleration but not accelleration above max. 
            {moveVec.x += apply; print("2");}
        else if(moveVec.x <= -maxAirSpeed*Time.fixedDeltaTime)
            {moveVec.x += apply; print("3");}
        else
            print("4");

    }

    //moveVec
    void ApplyGravity() //rn gravity is linear
    {
        if(currentGravity == 0 || gravityOn ==false )
            print("Apply Gravity being called even though scale is " +
                gravityScale + " and gravity On is " + gravityOn);
        if(gravityOn && moveVec.y >= -fallSpeedCap*Time.fixedDeltaTime)
            moveVec += Vector2.down * currentGravity * Time.fixedDeltaTime;
    }

    //grounded
    void CheckGrounded()
    {
        grounded = CheckBoxOverlap(transform.position + Vector3.up*GroundCheckOffset, GroundCheckBounds, 0f, ground);
    }

    //roofed
    void CheckRoofed()
    {
        roofed = CheckBoxOverlap( transform.position + Vector3.up*RoofCheckOffset, RoofCheckBounds, 0f, roof);
    }

    bool CheckBoxOverlap( Vector2 center, Vector2 size, float rotation, LayerMask checkLayers)
    {
        ContactFilter2D c = new ContactFilter2D();
        c.layerMask = roof;
        c.useLayerMask = true;
        return Physics2D.OverlapBox(center,size, rotation,c,colliders) > 0;
    }

    //gravityScale , lastGravity
    void GravityOn()
    {
        currentGravity = gravityScale;
    }

    //gravityScale, lastGravity
    void GravityOff()
    {
        currentGravity = 0;
    }

    //gravityScale, lastGravity
    void SetGravity(float g)
    {
        if(g!=currentGravity)
        {
            currentGravity = g;
        }
    }

    void ResetGravity()
    {
        currentGravity = gravityScale;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + Vector3.up*GroundCheckOffset , GroundCheckBounds);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up*RoofCheckOffset , RoofCheckBounds);
    }


}
