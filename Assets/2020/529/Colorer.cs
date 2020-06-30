using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Colorer : MonoBehaviour
{

    public Color color;
    Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().sharedMaterial.color = color;
        currentColor = color;    
    }

    public void OnEnable()
    {
        GetComponent<MeshRenderer>().sharedMaterial.color = color;    
        currentColor = color;
    }

    public void OnDisable()
    {
        GetComponent<MeshRenderer>().sharedMaterial.color = new Color(1,1,1,1);
        currentColor.r = 1; currentColor.g = 1; currentColor.b = 1; currentColor.a = 1;

    }

    public void ChangeColor(Color c)
    {
        color = c;
        GetComponent<MeshRenderer>().sharedMaterial.color = color;
        currentColor = color;   
    }

    // Update is called once per frame
    void Update()
    {
        if(color != currentColor) OnEnable();
    }

}
