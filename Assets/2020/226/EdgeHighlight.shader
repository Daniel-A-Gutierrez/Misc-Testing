// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/EdgeHighlight"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        [HDR]_Color("Main Color", Color) = (1,1,1,1)
        _LineWidth("LineWidth" , float) = .25
        _LineSpacing("LineSpacing", float) = 1.0
        _LineOffset("LineOffset" , Vector) = (0.0,0.0,0.0,0.0)
    }
    SubShader
    {
          Tags { "Queue"="Transparent" "Render"="Transparent" "IgnoreProjector"="True"}
          LOD 100
          
          ZWrite Off
          Blend SrcAlpha OneMinusSrcAlpha

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
                float4 worldpos : TEXCOORD1;
            };

            float4 _Color;
            float _LineWidth;
            float _LineSpacing;
            float4 _LineOffset;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldpos = mul(unity_ObjectToWorld, v.vertex);//  mul (v.vertex,unity_ObjectToWorld );
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                //float4 col = float4(i.uv, 0.0,1.0);
                //float4 col = _Color* step(  min(i.uv.x,i.uv.y) , .1);
                //if x or y is below .1 or above .9 , set true. 
                //what if its just x ? 
                // step ( .9, x) or step(x,.1) 
                //alt abs(x-.5) in the range of .4 to .5 which translates to 
                // abs(x-.5) -.4 >= .1
                //and to combine the terms with an max them

                //float4 col = _Color * max(step(.025, abs(i.uv.x - .5) - .45), step(.025, abs(i.uv.y - .5) - .45));
                float4 dist = fmod(abs(i.worldpos - _LineOffset), _LineSpacing);
                bool comp = any(step(_LineWidth, dist)) | any(step(_LineWidth, _LineSpacing-dist)) ;
                return _Color*comp;
            }
            ENDCG
        }
    }
}
