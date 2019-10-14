Shader "Unlit/PinTableTransitionShader"
{
	Properties
	{
		_NextTex("Next Texture", 2D) = "white" {}
		_CurrentTex("Current Texture", 2D) = "white" {}
		_TransitionTex("Transition Texture", 2D) = "white" {}
		_DisplacementFactor("Displacement Factor", Float) = 0.0
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		CGPROGRAM
		#pragma surface surf Standard vertex:vert

		struct Input
		{
			float2 uv_NextTex;
			fixed4 color;
		};

		sampler2D _NextTex;
		sampler2D _CurrentTex;
		sampler2D _TransitionTex;
		float _DisplacementFactor;
		float4 _Color;

		void vert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			float2 uv = v.texcoord;
			float transition = tex2Dlod(_TransitionTex, float4(uv, 0, 0)).r;
			v.vertex.y += lerp(tex2Dlod(_NextTex, float4(uv, 0, 0)).r, tex2Dlod(_CurrentTex, float4(uv, 0, 0)).r, transition) * _DisplacementFactor * v.color.r;
			data.color = v.color;
		}

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = lerp(tex2D(_NextTex, IN.uv_NextTex), tex2D(_CurrentTex, IN.uv_NextTex), tex2D(_TransitionTex, IN.uv_NextTex).r) * _Color;
		}
		ENDCG
	}
}
