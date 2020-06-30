using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mirror;

[ExecuteInEditMode]
public class NetColorer : NetworkBehaviour
{
    [SerializeField]
    GameObject target;
    [SyncVar]
    public Color color;
    [SyncVar]
    Color currentColor;
    private Material m;

    private void Awake()
    {
        if (target == null)
            target = gameObject;
        m = new Material(Shader.Find("Standard"));
        m.SetColor("_Color", color);
        target.GetComponent<MeshRenderer>().material = m;
    }

    // Start is called before the first frame update
    void Start()
    {

        //target.GetComponent<MeshRenderer>().sharedMaterial.color = color;
        m.SetColor("_Color", color);
        currentColor = color;
    }

    public void OnEnable()
    {
        //target.GetComponent<MeshRenderer>().sharedMaterial.color = color;
        m.SetColor("_Color", color);
        currentColor = color;
    }

    public void OnDisable()
    {
        //target.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(1, 1, 1, 1);
        m.SetColor("_Color", new Color(1, 1, 1, 1));
        currentColor.r = 1; currentColor.g = 1; currentColor.b = 1; currentColor.a = 1;

    }

    public void ChangeColor(Color c)
    {
        color = c;
        //target.GetComponent<MeshRenderer>().sharedMaterial.color = color;
        m.SetColor("_Color", color);
        currentColor = color;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
            target = gameObject;
        if (color != currentColor) OnEnable();
    }

}

