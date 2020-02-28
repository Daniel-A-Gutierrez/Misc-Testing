using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBloom : MonoBehaviour
{
    public ComputeShader blur;
    public float screenScaling =1.0f;
    public float radius;
    public float threshhold;
    RenderTexture Input;
    public RenderTexture Result;
    //public Material mat;
    Camera cam;
    int srcWidth;
    int srcHeight;
    int kernel;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        srcWidth = cam.pixelWidth;
        srcHeight = cam.pixelHeight;
        kernel = blur.FindKernel("CSMain");

        blur.SetFloat("radius",1.0f);
        Result = new RenderTexture((int)(cam.pixelWidth*screenScaling),
            (int)(cam.pixelHeight*screenScaling),1,RenderTextureFormat.ARGBFloat);
        Result.enableRandomWrite = true;
        Result.Create(); //THIS WAS WHAT FIXED IT.
        blur.SetTexture(kernel,"Result",Result);
        cam.targetTexture = null;
        blur.SetInt("width", srcWidth);
        blur.SetInt("height", srcHeight);
        blur.SetFloat("radius", 4);
        blur.SetFloat("threshhold", 1.1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        blur.SetFloat("time", Time.time);
    }
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        blur.SetTexture(kernel,"_Input",source);
        blur.Dispatch(kernel,Result.width/8,Result.height/8,1);
        Graphics.Blit(Result,dest);
    }
}

//basic idea of blur : average of the n pix around it.
//basic idea of bloom : take residual of image to get just the bright stuff, 
//      blur the residual, add it back to the normal
//i could also have it resample the same texture every frame to like, leave a trail.