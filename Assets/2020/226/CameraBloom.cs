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
    public Material mat;
    Camera cam;
    int srcWidth;
    int srcHeight;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        srcWidth = cam.pixelWidth;
        srcHeight = cam.pixelHeight;

        blur.SetFloat("radius",1.0f);
        Result = new RenderTexture((int)(cam.pixelWidth*screenScaling),
            (int)(cam.pixelHeight*screenScaling),1,RenderTextureFormat.ARGBFloat);
        Result.enableRandomWrite = true;
        blur.SetTexture(0,"Result",Result);

        
    }

    // Update is called once per frame
    void Update()
    {
        
        blur.Dispatch(0,Result.width/8,Result.height/8,1);
        mat.SetTexture("Texture", Result);
        
    }
    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        blur.SetTexture(0,"Input",source);
        Graphics.Blit(source,dest,mat);
    }
}

//basic idea of blur : average of the n pix around it.
//basic idea of bloom : take residual of image to get just the bright stuff, 
//      blur the residual, add it back to the normal
//i could also have it resample the same texture every frame to like, leave a trail.