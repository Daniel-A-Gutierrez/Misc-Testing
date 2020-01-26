using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    ParticleSystem ps;
    Color last;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        last = main.startColor.color ;

    }
    
    // Update is called once per frame
    public void Change()
    {
        var main = ps.main;
        last =  new Color(1f-last.r, 1f-last.g,1f-last.b);
        main.startColor = last;
    }
}
