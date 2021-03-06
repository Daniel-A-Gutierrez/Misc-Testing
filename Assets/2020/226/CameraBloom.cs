﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBloom : MonoBehaviour
{
    public Shader residual;
    public Shader additive;
    public Shader blurShader;
    public float threshhold;
    public float intensity;
    [Range(0,1)]
    public int FlipX;

    //gaussian blur stuff
    public int downscale = 3;
    public int iterations = 4;
    public float sigma = 2.0f;
    public float blurSize = 2.0f;
    ///<summary>Stick to multiples of 4 +1 </summary>
    public float blurSamples = 17.0f;

    RenderTexture TrailLight;
    public RenderTexture Result;
    public RenderTexture Light;
    //public Material mat;
    Camera cam;
    int srcWidth;
    int srcHeight;
    int kernel;


    Material blurFilter;
    Material residualFilter;
    Material additiveFilter;

    Animator animator;

    //plan : take input from camera -> blit input using residual (can be done at lower res) into LIGHT -> blur LIGHT and put it back into LIGHT-> slap it onto RESULT using selective bloom-> blit it out 

    // Start is called before the first frame update
    void Start()
    {

        blurFilter = new Material(blurShader);
        residualFilter = new Material(residual);
        additiveFilter = new Material(additive);

        cam = GetComponent<Camera>();
        animator = GetComponent<Animator>();
        srcWidth = cam.pixelWidth;
        srcHeight = cam.pixelHeight;
        cam.targetTexture = null;

        Light = new RenderTexture((cam.pixelWidth>>downscale),
            (cam.pixelHeight>>downscale),1,RenderTextureFormat.ARGBFloat);
        Light.Create(); //THIS WAS WHAT FIXED IT.

        Result = new RenderTexture((cam.pixelWidth),
            (cam.pixelHeight),1,RenderTextureFormat.ARGBFloat);
        Result.Create();

        TrailLight = new RenderTexture((cam.pixelWidth >> downscale),
            (cam.pixelHeight >> downscale), 1, RenderTextureFormat.ARGBFloat);
        TrailLight.Create();
        



        blurFilter.SetInt("_Width", srcWidth);
        blurFilter.SetInt("_Height", srcHeight);
        blurFilter.SetFloat("sigma", sigma);
        blurFilter.SetFloat("blurSize", blurSize);
        blurFilter.SetFloat("blurSamples", blurSamples);
        
        additiveFilter.SetTexture("_Light",Light);
        additiveFilter.SetFloat("_Intensity", intensity);

        residualFilter.SetFloat("_Threshhold", threshhold);
        residualFilter.SetTexture("_TrailTex", TrailLight);
        
    }

    // Update is called once per frame
    void Update()
    {
        DebugUpdate();
    }

    void DebugUpdate()
    {
        //blurFilter.SetInt("_Width", srcWidth);
        //blurFilter.SetInt("_Height", srcHeight);
        blurFilter.SetFloat("sigma", sigma);
        blurFilter.SetFloat("blurSize", blurSize);
        blurFilter.SetFloat("blurSamples", blurSamples);
        
        //additiveFilter.SetTexture("_Light",Light);
        additiveFilter.SetFloat("_Intensity", intensity);

        residualFilter.SetFloat("_Threshhold", threshhold);
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            Flip();
        

    }

    bool flipped = false;
    void Flip()
    {
        if(flipped)
        {
            animator.Play("Unflip");
            flipped = false;
            additiveFilter.SetInt("_FlipX", 0);
        }
        else
        {
            animator.Play("Flip");
            flipped = true;
            additiveFilter.SetInt("_FlipX", 1);

        }
    }
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        //so i have blur now. I want to take the original image, take its residual, blur that, and add it to the original image after.
        Graphics.Blit(Light, TrailLight);
        Graphics.Blit(source,Light,residualFilter);
        ApplyGaussianBlur(Light,Light);
        Graphics.Blit(source,dest,additiveFilter);

        // blur.SetTexture(kernel,"_Input",source);
        // blur.Dispatch(kernel,Result.width/8,Result.height/8,1);
        // Graphics.Blit(Result,dest);
    }
    //how might i blur it... i mean i should only blur the light i guess. Maybe instead of lerping it down. flatly subtract an amount

    public void ApplyGaussianBlur(RenderTexture src, RenderTexture dst)
    {
        blurFilter.SetInt("_Width",src.width );
        blurFilter.SetInt("_Height",src.width );
        blurFilter.SetFloat("sigma", sigma);
        blurFilter.SetFloat("blurSize", blurSize);
        blurFilter.SetFloat("blurSamples", blurSamples);

        RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height , 0, RenderTextureFormat.ARGBFloat);
        RenderTexture temp2 = RenderTexture.GetTemporary(src.width, src.height, 0 , RenderTextureFormat.ARGBFloat);
        temp.filterMode = FilterMode.Bilinear;
        temp.filterMode = FilterMode.Bilinear;
        

        Graphics.Blit(src,temp);
        for(int i = 0 ; i < iterations ; i++)
        {
            Graphics.Blit(temp,temp2,blurFilter);
            Graphics.Blit(temp2,temp,blurFilter);
        }
        Graphics.Blit(temp,dst);
        RenderTexture.ReleaseTemporary( temp);
        RenderTexture.ReleaseTemporary( temp2);
    }


}

//basic idea of blur : average of the n pix around it.
//basic idea of bloom : take residual of image to get just the bright stuff, 
//      blur the residual, add it back to the normal
//i could also have it resample the same texture every frame to like, leave a trail.
//so i could take light - the residual of the input - and stack it every frame onto trail, cap it at some value,
//and then reduce it every frame by a fixed amount

