Shader "Custom/Dithering"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _DitherTex("Dither Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            TEXTURE2D(_MainTex);
            TEXTURE2D(_DitherTex);
            SAMPLER(sampler_MainTex);
            SAMPLER(sampler_DitherTex);

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _DitherTex_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.vertex = TransformObjectToHClip(http://IN.vertex.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color = IN.color;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 ditherUV = IN.uv * IN.color.a;
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                float dither = SAMPLE_TEXTURE2D(_DitherTex, sampler_DitherTex, ditherUV).r;
                col.rgb = step(dither, 0.5);
                col.a = IN.color.a;
                return col;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}