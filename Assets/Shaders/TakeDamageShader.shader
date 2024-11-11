Shader "Custom/TakeDamageShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (0,0,0)
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _OcclusionMap ("Occlusion Map", 2D) = "white" {}
        _MetallicGlossMap ("Metallic Gloss Map", 2D) = "white" {}
        _BlinkIntensity ("Blink Intensity", Range(0,1)) = 0.4
        _BlinkInterval ("Blink Interval", Range(0,10)) = 1.5
        _BlinkTrigger ("Blink Trigger", Range(0,1)) = 0.0
        _BlinkTime ("Blink Time", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        sampler2D _EmissionMap;
        sampler2D _BumpMap;
        sampler2D _OcclusionMap;
        sampler2D _MetallicGlossMap;
        fixed4 _Color;
        fixed4 _EmissionColor;
        half _Metallic;
        half _Smoothness;
        half _BlinkIntensity;
        half _BlinkInterval;
        half _BlinkTrigger;
        half _BlinkTime;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_EmissionMap;
            float2 uv_BumpMap;
            float2 uv_OcclusionMap;
            float2 uv_MetallicGlossMap;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // Metallic and Smoothness from MetallicGlossMap
            fixed4 mg = tex2D(_MetallicGlossMap, IN.uv_MetallicGlossMap);
            o.Metallic = mg.r * _Metallic;
            o.Smoothness = mg.a * _Smoothness;

            // Occlusion from OcclusionMap
            fixed occlusion = tex2D(_OcclusionMap, IN.uv_OcclusionMap).r;
            o.Occlusion = occlusion;

            // Emission from EmissionMap
            o.Emission = tex2D(_EmissionMap, IN.uv_EmissionMap).rgb * _EmissionColor.rgb;

            // Calculate the blink factor using a sine wave
            float time = _Time.y / _BlinkInterval;
            float blink = 0.5 + 0.5 * sin(time * UNITY_PI * 2);
            if (_BlinkTrigger > 0)
            {
                o.Emission += fixed3(blink * _BlinkIntensity, 0, 0); // Blink in red
                _BlinkTime += _Time.y - _BlinkTime; // Calculate delta time
                if (_BlinkTime >= _BlinkInterval)
                {
                    _BlinkTrigger = 0; // Reset the trigger after one blink
                    _BlinkTime = 0; // Reset the blink time
                }
            }
        }
        ENDCG
    }
    FallBack "Standard"
}
