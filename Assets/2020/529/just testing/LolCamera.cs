using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LolCamera : MonoBehaviour
{

    //camera angle is constant , middle click and drag to move, scroll to zoom in and out. 
    //press space to move camera over base
    public Vector3 basePosition;
    float offset;
    Vector2 lastMousePosition;
    public Vector2 sensitivity;
    Vector3 BL;
    Vector3 TR;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        bool contact = Physics.Raycast(transform.position, transform.forward, out hit);
        offset = hit.distance;//contact ? hit.distance : math to figure out distance to xz plane that i dont need rn
        TR = Camera.main.ScreenPointToRay( new Vector3(Camera.main.pixelWidth -1, Camera.main.pixelHeight,0)).GetPoint(offset);
        BL = Camera.main.ScreenPointToRay(Vector2.zero).GetPoint(offset);
        sensitivity = (new Vector2(TR.x,TR.z) - new Vector2(BL.x,BL.z)) / (new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        print(offset);
        print(new Vector2(BL.x, BL.z));
        print(new Vector2(TR.x, TR.z));
        print(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            transform.position = basePosition - offset * transform.forward;
        if(Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            Vector2 mp = Input.mousePosition;
            Vector2 delta = mp - lastMousePosition;
            lastMousePosition = mp;
            transform.position -= new Vector3(delta.x * sensitivity.x, 0 , delta.y * sensitivity.y) ;
        }
    }
}
