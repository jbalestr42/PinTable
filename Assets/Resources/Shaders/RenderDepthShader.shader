﻿Shader "Custom/RenderDepth"
{
	Properties
	{
		[HideInInspector]_MainTex("Texture", 2D) = "white" {}
		 _Factor("Factor to apply to the depth", Float) = 1
	}

	SubShader
	{
		// markers that specify that we don't need culling 
		// or reading/writing to the depth buffer
		Cull Off
		ZWrite Off
		ZTest Always

		Pass
		{
			CGPROGRAM
			//include useful shader functions
			#include "UnityCG.cginc"

			//define vertex and fragment shader
			#pragma vertex vert
			#pragma fragment frag

			//texture and transforms of the texture
			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			float _Factor;

			//the object data that's put into the vertex shader
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			//the data that's used to generate fragments and can be read by the fragment shader
			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//the vertex shader
			v2f vert(appdata v)
			{
				v2f o;
				//convert the vertex positions from object space to clip space so they can be rendered
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				return o;
			}

			//the fragment shader
			fixed4 frag(v2f i) : SV_TARGET
			{
				//get depth from depth texture
				float depth = tex2D(_CameraDepthTexture, i.uv).r;
				#if !defined(UNITY_REVERSED_Z)
				depth = 1.0f - depth;
				#endif
				return depth * _Factor;
			}
			ENDCG
		}
	}
}