Shader "Custom/DistanceColor"
{
    Properties
    {
         _MainTex("Base (RGB)", 2D) = "white" {}
         _ClosestColor("Particle color at its closest", Color) = (1, 0, 0, 1)
         _InBetweenColor("Particle color at a distance", Color) = (0, 1, 0, 1)
         _FarthestColor("Particle color at its furthest", Color) = (0, 0, 1, 1)


         _FirstDistance("First Distance", Float) = 1
         _SecondDistance("Second Distance", Float) = 2
         _ThirdDistance("Third Distance", Float) = 4

        _Threshold("Threshold", Range(0.0, 1.0)) = 1.
    }
        SubShader
         {
            Tags {"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
              ZTest False
              ZWrite Off
              Blend OneMinusDstColor One

             CGPROGRAM
             #pragma surface surf Lambert

             sampler2D _MainTex;

             struct Input
             {
                 float2 uv_MainTex;
                 float3 worldPos;
             };

             float _FirstDistance;
             float _SecondDistance;
             float _ThirdDistance;

             half4 _ClosestColor;
             half4 _InBetweenColor;
             half4 _FarthestColor;

             half4 _distanceColor;
             float _Threshold;

             void surf(Input IN, inout SurfaceOutput o)
             {
                 half4 c = tex2D(_MainTex, IN.uv_MainTex);
                 float dist = distance(_WorldSpaceCameraPos, IN.worldPos);

                if (dist < _FirstDistance) {
                    _distanceColor = _ClosestColor;
                }

                if (dist >= _FirstDistance && dist < _SecondDistance) {
                    half weight = (dist - _FirstDistance) / (_SecondDistance - _FirstDistance);
                    _distanceColor = lerp(_ClosestColor, _InBetweenColor, weight);
                }

                if (dist >= _SecondDistance && dist < _ThirdDistance) {
                    half weight = (dist - _SecondDistance) / (_ThirdDistance - _SecondDistance);
                    _distanceColor = lerp(_InBetweenColor, _FarthestColor, weight);
                }

                if (dist >= _ThirdDistance) {
                    _distanceColor = _FarthestColor;
                }
                 o.Albedo = c.rgb * _distanceColor.rgb;
                 o.Alpha = c.a;
                 o.Emission = c.rgb * pow(_distanceColor.rgb, _Threshold);
             }

            ENDCG
         }
             FallBack "Diffuse"
}