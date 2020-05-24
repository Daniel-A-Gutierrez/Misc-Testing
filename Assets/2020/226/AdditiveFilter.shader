Shader "Unlit/AdditiveFilter"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Light ("Texture", 2D) = "white" {}
        _Intensity("Intensity" , float) = 1.0
        _FlipX("FlipX", int) = 0
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
            sampler2D _Light;
            float _Intensity;
            int _FlipX;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                if (_FlipX==1)
                {
                    o.uv.x = 1 - o.uv.x;
                }
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv) + _Intensity*tex2D(_Light,i.uv);
                if (_FlipX == 1)
                    col = 1.0 - col;
                return col;
            }
            ENDCG
        }
    }
}
