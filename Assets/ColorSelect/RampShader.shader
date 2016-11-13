// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "RampShader"
{
	Properties{
		_Ramp("Ramp", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader{
		Pass
		{
			Tags{ "LightMode" = "ForwardBase" } // So diffuse lighting will always face the correct direction
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc" // For UnityObjectToWorldNormal

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed4 diffuseDot : COLOR0;
				fixed4 viewDot : COLOR1;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.diffuseDot = (dot(worldNormal, _WorldSpaceLightPos0.xyz) + 1) / 2;

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				o.viewDot = abs(dot(worldNormal, worldViewDir));

				return o;
			}

			sampler2D _Ramp;
			float4 _Color;
				
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_Ramp, i.diffuseDot.r * i.viewDot.r) * _Color;
				return col;
			}

			ENDCG			
		}


			Pass
			{
				Name "ShadowCaster"
				Tags{ "LightMode" = "ShadowCaster" }

				Fog{ Mode Off }
				ZWrite On ZTest LEqual Cull Off
				Offset 1, 1

				CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_shadowcaster
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

				struct v2f {
				V2F_SHADOW_CASTER;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				TRANSFER_SHADOW_CASTER(o)
					return o;
			}

			float4 frag(v2f i) : COLOR
			{
				SHADOW_CASTER_FRAGMENT(i)
			}
				ENDCG
			}
	}
}
