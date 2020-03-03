Shader "Unlit/DownsampleBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width ("Input Width" , int) = 0
        _Height ("Input Height" , int) = 0
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
            int _Width;
            int _Height;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 pixSize = 1.0/float2(_Width,_Height);
                float4 col =tex2D(_MainTex, i.uv + float2(pixSize.x,0) ) +
                            tex2D(_MainTex, i.uv + float2(-pixSize.x,0) ) +
                            tex2D(_MainTex, i.uv + float2(0,pixSize.y) ) +
                            tex2D(_MainTex, i.uv + float2(0,-pixSize.y) );
                //fixed4 col = tex2D(_MainTex, i.uv + float2(pixSize.x,0) );
                return col /4.0;
            }
            ENDCG
        }
    }
}



//gaussian blur
