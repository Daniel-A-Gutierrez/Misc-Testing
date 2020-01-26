using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TopDownMove : MonoBehaviour
{
    /*Testing collisions here with animations : this movement behaves as I want, move and slide style. */

    /*
    
    STATES          BEHAVIORS
    DefaultState    SetMoveDirection
    DashState       DashState
    
     */
    
    Dictionary<string,Action> States; 
    public float speed;
    Vector2 moveVec;
    string state;


    //DASHING
    public float dashDistance;
    public float dashTime;
    


    //monobehavior functions

    void Awake()
    {
        States = new Dictionary<string,Action>(); 
        state= "DefaultState";
        States["DefaultState"] = DefaultState;
        States["DashingState"] = DashingState;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveVec = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        States[state]();
    }

    void FixedUpdate()
    {
        if(moveVec!= Vector2.zero)
            GetComponent<Rigidbody2D>().MovePosition( (Vector2)(transform.position) + moveVec*speed);
    }

    //STATES

    //      DEFAULT
    void EnterDefaultState()
    {
        state = "DefaultState";
        DefaultState();
    }

    void DefaultState()
    {
        SetMoveDir();    

        if(Input.GetKeyDown(KeyCode.Space) & moveVec != Vector2.zero )
        {
            ExitDefaultState();
            EnterDashingState();
        }

    }

    void ExitDefaultState()
    {
        ZeroMoveDir();
    }


        //DASH
    float Dash_StartTime;
    Vector2 Dash_InitialPosition;
    Vector2 Dash_Direction;
    void EnterDashingState()
    {
        Dash_StartTime = Time.time;
        Dash_InitialPosition = transform.position;
        SetMoveDir();
        Dash_Direction = moveVec;
        ZeroMoveDir();
        state = "DashingState";
        DashingState();
    }

    void ExitDashingState()
    {
        ZeroMoveDir();
    }

    void DashingState()
    {
        GetComponent<Rigidbody2D>().MovePosition(
            Vector2.Lerp(Dash_InitialPosition, Dash_InitialPosition + Dash_Direction*dashDistance, (Time.time-  Dash_StartTime)/dashTime ));
        if( (Time.time-  Dash_StartTime)/dashTime >1)
        {
            ExitDashingState();
            EnterDefaultState();
        }
    }


    //FUNCTIONS



    void ZeroMoveDir()
    {
        moveVec= Vector2.zero;
    }

    void SetMoveDir()
    {
        moveVec = ((Input.GetKey(KeyCode.A) ? Vector2.left : Vector2.zero) +
         (Input.GetKey(KeyCode.D) ? Vector2.right : Vector2.zero) + 
         (Input.GetKey(KeyCode.W) ? Vector2.up : Vector2.zero) +
         (Input.GetKey(KeyCode.S) ? Vector2.down : Vector2.zero)).normalized;
    }


}
