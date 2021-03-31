using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCameraFollow : MonoBehaviour
{

    public Transform target;
    public float cameraSensitivity;
    public float stiffness = 0.5f;
    public float damping = 0.5f;

    public float angularStiffness = 0.1f;
    public float angularDamping = 0.1f;

    private float maxDistance;
    private Vector3 offset;

    private Vector3 velocity;
    private Vector3 position;
    private Vector3 targetPosition;

    private float angularVelocity = 0.0f;
    private float angle = 0.0f;
    private Vector3 up;
    private Quaternion targetRotation;

    Vector3 lastMousePosition;
    void Start()
    {   
        // Store initial offset
        offset = transform.position - target.transform.position;

        // Max distance is the default distance
        maxDistance = offset.magnitude;

        position = transform.position;
        // rotation = transform.rotation;
        lastMousePosition = Input.mousePosition;
    }

    public void LateUpdate()
    {
        //targetPosition = target.TransformPoint(offset);
        targetPosition = target.position + offset;
        Vector3 targetOffset = targetPosition - position;

        Vector3 smoothVelocity = targetOffset * stiffness;
        smoothVelocity -= velocity * damping;
        velocity += smoothVelocity;
        position += velocity; 
        
        // if (Vector3.Distance(position, target.position) > maxDistance){
        //     Vector3 newPosition = (position - target.position).normalized * maxDistance;
        //     velocity = newPosition - position;
        // }

        transform.position = position;

        // Rotation

        transform.forward = (target.position - transform.position).normalized;


        if(Input.GetMouseButton(1))
        {
            Quaternion rot_h = Quaternion.AngleAxis(cameraSensitivity*(Input.mousePosition.x-lastMousePosition.x) , Vector3.up);
            offset = rot_h*offset;
        }
        


        // // Get the angle between the current direction and the velocity direction
        // float offsetAngle = Vector3.Angle(target.up, up);
        // float sign = Mathf.Sign(Vector3.Cross(up, target.up).z);
        // offsetAngle *= sign * angularStiffness;
        // offsetAngle -= angularVelocity * angularDamping;

        // angularVelocity += offsetAngle;
        // angle += angularVelocity;
        // // up = Vector3.RotateTowards(up, target.up, angularVelocity, 0.0f);


        // Quaternion rotation = Quaternion.AxisAngle(Vector3.forward, angle);
        // // Apply the rotation over time
        // transform.rotation = rotation;
        // up = transform.up;

        // targetRotation = target.rotation;
        // Quaternion rotationOffset = targetRotation * Quaternion.Inverse(rotation);
        // Quaternion smoothRotation = Quaternion.Slerp(
        //     Quaternion.identity, 
        //     rotationOffset, 
        //     angularStiffness
        // );
        // smoothRotation = Quaternion.Normalize(smoothRotation);

        // smoothRotation *= Quaternion.Slerp(
        //     Quaternion.identity, Quaternion.Inverse(angularVelocity), 
        //     angularDamping
        // );
        // smoothRotation = Quaternion.Normalize(smoothRotation);

        // angularVelocity *= smoothRotation;
        // rotation *= angularVelocity;
        // transform.rotation = rotation;
        lastMousePosition = Input.mousePosition;
    }
}
