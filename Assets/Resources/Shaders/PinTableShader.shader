Shader "Unlit/PinTableShader"
{
	Properties
	{
		_DisplacementTex("Displacement Texture", 2D) = "white" {}
		_CameraTex("Displacement Texture", 2D) = "white" {}
		_DisplacementFactor("Displacement Factor", Float) = 1.0
		_PlatformFactor("Platform Factor", Float) = 1.0
		_Speed("Speed", Float) = 1.0
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		CGPROGRAM
		#pragma surface surf Standard vertex:vert

		struct Input
		{
			float2 uv_DisplacementTex;
			float2 uv_CameraTex;
			fixed4 color;
		};

		sampler2D _DisplacementTex;
		sampler2D _CameraTex;

		fixed _DisplacementFactor;
		fixed _PlatformFactor;
		fixed _Speed;
		fixed4 _Color;

		void vert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			float2 uv = v.texcoord + float2(_Time[0], _Time[0]) * _Speed;
			v.vertex.y += tex2Dlod(_DisplacementTex, float4(uv, 0, 0)).r * _DisplacementFactor * v.color;
			v.vertex.y = lerp(v.vertex.y, v.color * _PlatformFactor, tex2Dlod(_CameraTex, v.texcoord).r);
			data.color = v.color;
		}

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = IN.color * lerp(_Color, 0.2, tex2D(_CameraTex, IN.uv_CameraTex));//tex2D(_DisplacementTex, IN.uv_DisplacementTex).rgb;
		}
		ENDCG
	}
}
