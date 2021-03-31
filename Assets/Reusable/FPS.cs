using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPS : MonoBehaviour
{
    TextMeshProUGUI text;
    public float updateperiod = .1f;
    float lastupdate = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - updateperiod > lastupdate)
        {
            lastupdate = Time.time;
            text.text = "" +  (int)(1f/Time.deltaTime);
        }
    }
}
