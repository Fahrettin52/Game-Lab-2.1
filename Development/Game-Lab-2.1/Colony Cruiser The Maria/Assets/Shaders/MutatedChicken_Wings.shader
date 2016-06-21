Shader "Custom/MutatedChicken_Wings" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic (R)", 2D) = "black" {}
		_Normal ("Normal (RGB)", 2D) = "bump" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Cull Off
		
		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Metallic;
		sampler2D _Normal;		

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			half4 m = tex2D (_Metallic, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Metallic = m.r;
			o.Smoothness = m.a;
			o.Normal = UnpackNormal (tex2D (_Normal, IN.uv_MainTex));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
