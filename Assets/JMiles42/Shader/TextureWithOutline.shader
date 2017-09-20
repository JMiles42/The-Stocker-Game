// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "JMiles42/Texture With Outline"
{
	Properties
	{
		_Colour("Colour",color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white"{}
		_Outline("Outline Width",Range(0,10)) = 0
		_OutlineCol("Outline Colour",color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
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
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};
	
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Colour;
			float _Offset;
	
			v2f vert(appdata v)
			{
				v2f o;
				v.vertex.xyz += v.normal.xyz * _Offset;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float _Outline;
			float4 _OutlineCol;

			v2f vert(appdata v)
			{
				v2f o;
				v.vertex.xyz += v.normal.xyz * _Outline;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _OutlineCol;
			}
				ENDCG
		}
	}
}
