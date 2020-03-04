﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBloom2 : MonoBehaviour
{
    public int iterations;
    public int downscale;
    private Material blurFilter;
    public Shader blurShader;
    // Start is called before the first frame update
    void Start()
    {
        //Shader.EnableKeyword("HORIZONTAL_BLUR_9"); doesnt work
        blurFilter = new Material(blurShader);
        //so if its global, does tat mean i cant make a horiz blur shader and a vert blur shader? maybe diff kernels?
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        blurFilter.SetInt("_Width",src.width>>downscale );
        blurFilter.SetInt("_Height",src.width>>downscale );
        blurFilter.SetFloat("sigma", 2.0f);
        blurFilter.SetFloat("blurSize", 2.0f);
        blurFilter.SetFloat("blurY", 0.0f);
        blurFilter.SetFloat("blurX", 1.0f);
        blurFilter.SetFloat("blurSamples", 17.0f);

        RenderTexture temp = RenderTexture.GetTemporary(src.width>>downscale, src.height>>downscale , 0, RenderTextureFormat.ARGBFloat);
        RenderTexture temp2 = RenderTexture.GetTemporary(src.width>>downscale, src.height>>downscale, 0 , RenderTextureFormat.ARGBFloat);
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

/*
Shader.EnableKeyword("MYDEFINE")
Shader.DisableKeyword

and use them in your shader as;
#if defined(MYDEFINE)
...etc
#endif
*/