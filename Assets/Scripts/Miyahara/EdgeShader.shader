Shader "Custom/EdgeShader" {
Properties 
    {
        _Color("Color", Color) = (1,1,1,1)
        _EdgeColor("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _EdgeWidth("EdgeWidth", Range(0, 0.3)) = 0.02
    }
    SubShader 
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGINCLUDE
       #include "UnityPBSLighting.cginc"
        
        half _Glossiness;
        half _Metallic;
        float4 _Color;
        float3 _EdgeColor;
        float _EdgeWidth;

        sampler2D _MainTex;

        struct Input 
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }

        void edgeVert(inout appdata_full v)
        {
            v.vertex.xyz += v.normal * _EdgeWidth;
        }

        void edgeSurf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _EdgeColor;
        }

        half4 LightingEdge(SurfaceOutputStandard s, half3 lightDir, half atten)
        {
            half4 c;
            c.rgb = s.Albedo * _LightColor0.rgb;
            c.a = s.Alpha;
            return c;
        }
        ENDCG

        Cull Back
        CGPROGRAM
       #pragma surface surf Standard fullforwardshadows
       #pragma target 3.0
        ENDCG

        Cull Front
        CGPROGRAM
       #pragma surface edgeSurf Edge fullforwardshadows vertex:edgeVert
       #pragma target 3.0

        ENDCG
    }
    FallBack "Diffuse"
}