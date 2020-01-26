Shader "Unlit/MandelbrotShader"
{
    Properties
    {
        _Iterations ("Iterations", Int) = 0 
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
                float4 worldpos : WORLD ;//this can be an arbitrary tag i guess
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _Iterations;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldpos = mul(unity_ObjectToWorld ,v.vertex);
                return o;
            }

            // void mainImage( out vec4 fragColor, in vec2 fragCoord )
            // {
            //     // Normalized pixel coordinates (from 0 to 1)
            //     vec2 uv = fragCoord/iResolution.xy;
            //     vec2 center = vec2(-sqrt(2.0f),0.0f);//  texture(iChannel0, vec2(3.)/iResolution.xy).xy;
            //     float zoom =  iTime;//texture(iChannel0, vec2(1.)/iResolution.xy).r;
            //     texture(iChannel0,vec2(5.0f)/iResolution.xy);
            //     vec2 lowerLeft = center- vec2(2.0f,0.8f)/pow(2.0f,zoom);
            //     vec2 upperRight = center + vec2(2.0f,0.8f)/pow(2.0f,zoom);
            //     int max_iterations = 100;
            //     int stability = 0;
            //     vec2 c = mix(lowerLeft,upperRight,uv); // coordinate space value
                
            //     vec2 test = vec2(0.0f);
            //     float root2 = sqrt(2.0f);
            //     for(stability = 0 ; stability < max_iterations ; stability++)
            //     {
            //         test = vec2(test.x *test.x - test.y*test.y, 2.0f*test.x*test.y )  + c;
            //         if( length(test) > 2.0f)
            //         {
            //         break;
            //         }
            //     }
            //     // Time varying pixel color
            //     //vec3 col =1.0f-vec3(float(stability)/float(1.0f)) ;

            //     // Output to screen
            //     fragColor = vec4(0.0f,1.0f-sqrt(float(stability)/float(max_iterations)),1.0f-float(stability)/float(max_iterations),1.0);

            // }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture


                int max_iterations = _Iterations;
                int stability = 0;
                float2 c = i.worldpos.xy;//mix(lowerLeft,upperRight,uv); // coordinate space value
                
                float2 test = float2(0.0,0.0);
                float root2 = sqrt(2.0);
                for(stability = 0 ; stability < max_iterations ; stability++)
                {
                    test = float2(test.x *test.x - test.y*test.y, 2.0*test.x*test.y )  + c;
                    if( length(test) > 2.0f)
                    {
                        break;
                    }
                }


                // Output to screen
                fixed4 col = fixed4(0.0,1.0-sqrt(float(stability)/float(max_iterations)),1.0-float(stability)/float(max_iterations),1.0);
                return col;
            }
            ENDCG
        }
    }
}
