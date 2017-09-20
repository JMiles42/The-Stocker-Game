// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "JMiles42/Glow Height"
{
	Properties
	{
		_TopColour("Top Colour",color) = (1,1,1,1)
		_BottomColour("Bottom Colour",color) = (1,1,1,1)
		_GradientBlendAmount("Gradient Blend Amount",float) = 1
	}
		SubShader
	{
		Tags
		{
			"RenderType" = "Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Off
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float3 worldPos : TEXCOORD0;
			};

			float4 _TopColour;
			float4 _BottomColour;
			float _GradientBlendAmount;
			
			v2f vert(appdata v)
			{
				v2f o;

				o.position = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = lerp(_BottomColour, _TopColour, (i.worldPos.y * _GradientBlendAmount));
				return col;
			}
			ENDCG
		}
	}
}
