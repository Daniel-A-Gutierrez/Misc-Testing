﻿Shader "Unlit/Residual"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshhold ("Threshhold"  , float) = 1.0
        _TrailTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _TrailTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = max(0.0, tex2D(_MainTex, i.uv) - 1.0); //dont get caught up rn, but later try to take the original color just if its bright enough.
                return col;
            }
            ENDCG
        }
    }
}
