Shader "Unlit/PinTableTransitionShader"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_NextTex("Next Texture", 2D) = "white" {}
		_TransitionTex("Transition Texture", 2D) = "white" {}
		_DisplacementFactor("Displacement Factor", Float) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		CGPROGRAM
		#pragma surface surf Standard vertex:vert

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
		};

		sampler2D _MainTex;
		sampler2D _NextTex;
		sampler2D _TransitionTex;
		float _DisplacementFactor;

		void vert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			float2 uv = v.texcoord;
			float transition = tex2Dlod(_TransitionTex, float4(uv, 0, 0)).r;
			v.vertex.y += lerp(tex2Dlod(_MainTex, float4(uv, 0, 0)).r, tex2Dlod(_NextTex, float4(uv, 0, 0)).r, transition) * _DisplacementFactor * v.color.r;
			data.color = v.color;
		}

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = lerp(tex2D(_MainTex, IN.uv_MainTex), tex2D(_NextTex, IN.uv_MainTex), tex2D(_TransitionTex, IN.uv_MainTex).r) * IN.color.r;
		}
		ENDCG
	}
}
