Shader "EasyRoads3D/ER Road" {
	Properties {
		[Space] [Header(Main Maps)] [Space] _MainTex ("Albedo", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_Metallic ("Metallic (R) AO (G) Smoothness (A)", 2D) = "gray" {}
		_MainMetallicPower ("Metallic Power", Range(0, 2)) = 0
		_MainSmoothnessPower ("Smoothness Power", Range(0, 2)) = 1
		_OcclusionStrength ("Ambient Occlusion Power", Range(0, 2)) = 1
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_BumpScale ("Normal Map Scale", Range(0, 4)) = 1
		[Space] [Space] [Header(Terrain Z Fighting Offset)] [Space] _OffsetFactor ("Offset Factor", Range(0, -10)) = -1
		_OffsetUnit ("Offset Unit", Range(0, -10)) = -1
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}