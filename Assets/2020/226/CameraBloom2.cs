using System.Collections;
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
        
        blurFilter = new Material(blurShader);
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        blurFilter.SetInt("_Width",src.width>>downscale );
        blurFilter.SetInt("_Height",src.width>>downscale );
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
