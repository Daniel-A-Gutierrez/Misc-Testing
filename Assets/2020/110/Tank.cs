using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    KeyCode LEFT = KeyCode.A;
    KeyCode FORWARD = KeyCode.W;
    KeyCode BACKWARD = KeyCode.S;
    KeyCode RIGHT = KeyCode.D;
    KeyCode SHOOT = KeyCode.Space;

    public float moveSpeed;
    public float rotationSpeed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float turn = (Input.GetKey(LEFT)?-1:0) + (Input.GetKey(RIGHT)?1:0);
        float fwd =  (Input.GetKey(FORWARD)?1:0) + (Input.GetKey(BACKWARD)?-1:0);
        rb.AddRelativeTorque(transform.up * turn*rotationSpeed,ForceMode.Acceleration);
        rb.MovePosition(rb.position + transform.forward*fwd*moveSpeed);

    }
}
