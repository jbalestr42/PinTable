Shader "Unlit/FlowMapShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_FlowTex("Flow Texture", 2D) = "white" {}
		_Factor("Scale", Float) = 0.0
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _FlowTex;
			float4 _MainTex_ST;
			float _Factor;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed2 displace = (tex2D(_FlowTex, i.uv).xy - fixed2(0.5, 0.5)) * 2.0 * _Factor * fixed2(-1, 1);
				fixed noise = tex2D(_FlowTex, i.uv);
				fixed flow = frac(_Time[1] + noise);
				fixed flowPhase = frac(_Time[1] + noise + 0.5);
				fixed transition = abs((flow - 0.5) * 2.0);
				fixed4 col = lerp(tex2D(_MainTex, i.uv + displace * flow), tex2D(_MainTex, i.uv + displace * flowPhase), transition);
				return col.r;
			}
			ENDCG
		}
	}
}
