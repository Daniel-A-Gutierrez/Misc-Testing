using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinButton : MonoBehaviour
{
    /*     --T-_---      */ 
    const float PI2 = Mathf.PI*2;
    public float period;
    float angularSpeed;

    //so by definition, the point on a wave at a given y will move at a velocity in z or x that is constant.
    //in logic this might look like
    /*
    t0 : notify block to go down
    t1 : block hits bottom, and notifies its targets to go down
    t2 : block continues its height function as per sin.

    im tempted to have each object hold a collection of "waves" which posses momentum and start time.
    i like that. Ill have them be called wave fronts. theyll hold amplitude, time, and the object that just passed them.

    i also need these things to be clocked. the state of these changes every 1/4 wavelength at fastest. so the clock ticks
    at startTime + 1/4*period*N . I could have wave fronts be passed whenever the time is that, and increment N
    
     */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class WaveFront
{

}