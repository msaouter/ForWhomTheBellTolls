Shader "Shaders/LilyPadShader"
{
	Properties
	{
		[Header(Shading)]
		_MainTex("Main Texture", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "green" {}
		_SSAO("Occlusion Map", 2D) = "white" {}
		_SpecularIntensity("Specular Intensity",Range(0.0,1.0)) = 0.5
		_Shininess("Shininess",Float) = 64.0
		_Transparency("Transparency",Range(0,1)) = 1.0
		_Amplitude("Amplitude",Range(0,10)) = 0.25
		_OffSetFactor("Factor OffSet",Range(0.0,0.5)) = 0.01

		_WindStrength("Wind Strength",Float) = 1
	}
		
		CGINCLUDE
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "Autolight.cginc"
		
		float _WindStrength;
		float _Amplitude;
		
		float _Id;
		float _Transparency;
		
		float _Shininess;
		float _SpecularIntensity;

		float _OffSetFactor;

		sampler2D _MainTex;
		float4 _MainTex_ST;
		sampler2D _NormalMap;
		float4 _NormalMap_ST;
		sampler2D _SSAO;
		float4 _SSAO_ST;


		static const float PI = 3.14159f;
		

		struct vertexInput
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 tangent : TANGENT;
			float4 uv : TEXCOORD0;
			uint id : SV_VertexID;
		};

		struct vertexOutput
		{
			float4 vertex : SV_POSITION;
			float4 tangent : TANGENT;
			float2 uv : TEXCOORD0;
			float3 viewDir : TEXTCOORD1;
			float3 worldNormal : NORMAL;
			uint id : TEXCOORD2;
			SHADOW_COORDS(3)
		};


		vertexOutput vert(vertexInput v)
		{
			vertexOutput o;
			//o.vertex = v.vertex;
			o.vertex = mul(unity_ObjectToWorld,v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			
			o.worldNormal = v.normal;
			o.viewDir = WorldSpaceViewDir(v.vertex);
			o.tangent = v.tangent;
			o.id = v.id;
			TRANSFER_SHADOW(o);
			return o;
		}
		

		[maxvertexcount(3)]
		void geo(triangle vertexOutput IN[3], inout TriangleStream<vertexOutput> triStream) {
			float4 pos;

			float2 offSet = float2(cos(_Id + _Time.y)*_OffSetFactor, sin(_Id + _Time.y)*_OffSetFactor);

			for (int i = 0; i < 3; i++) {
				pos = IN[i].vertex;
				pos.x += offSet.x;
				pos.z += offSet.y;
				pos.y += cos(_WindStrength*(_Time.y + _Id))*_Amplitude + _Amplitude;
				IN[i].worldNormal = UnityObjectToWorldNormal(IN[i].worldNormal);
				//IN[i].vertex = UnityObjectToClipPos(pos);
				IN[i].vertex = mul(UNITY_MATRIX_VP,pos);
				TRANSFER_SHADOW(IN[i]);
				triStream.Append(IN[i]);
			}
		}


		ENDCG

			SubShader{
			Pass
			{
				Tags
				{
					"BW" = "TrueProbes"
					"LightMode" = "UniversalForward"
					"PassFlags" = "OnlyDirectional"
					"Queue" = "Transparent"
					"RenderType" = "Transparent"
				}
				Blend SrcAlpha OneMinusSrcAlpha	
				
				
				CGPROGRAM
				#pragma vertex vert
				#pragma geometry geo
				#pragma fragment frag
				#pragma multi_compile_fwdbase
				#pragma target 4.6

				#include "Lighting.cginc"
				

				float4 frag(vertexOutput i) : SV_Target{
					float4 sample = tex2D(_MainTex, i.uv);
					float4 normal = tex2D(_NormalMap, (i.uv + _NormalMap_ST.zw) * _NormalMap_ST.xy);

					normal = normalize(normal * 2.0f - 1.0f);
					normal.xyz = normal.xzy;

					//float4 normal = (UnpackNormal(tex2D(_NormalMap, i.uv)),1.0f);
					float ambientOcclusion = tex2D(_SSAO,i.uv).r;
					float3 viewDir = normalize(i.viewDir);

					float shadow = SHADOW_ATTENUATION(i);


					//Blinn Phong avec half vector entre viewDir and Light
					float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);

					float NdotL = dot(_WorldSpaceLightPos0, normal);
					float NdotH = dot(normal, halfVector);
					//float NdotL = dot(normal, viewDir);
					float lightIntensity = smoothstep(0.0f, 0.01f, NdotL * shadow);

					float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
					float diff = max(0.0f,dot(normal.xyz, lightDir));
					float3 reflectDir = reflect(-lightDir, normal.xyz);
					float spec = pow(max(0.0f,dot(viewDir, reflectDir)), _Shininess);
					

					float3 light = lightIntensity * _LightColor0.rgb;
					//float3 light = _LightColor0.rgb;
					//simple
					//float3 ambient = light * sample.rgb;
					//with SSAO

					//float3 ambient =  sample.rgb *0.3f * ambientOcclusion;
					float3 ambient = UNITY_LIGHTMODEL_AMBIENT * 0.3f * ambientOcclusion;


					float3 diffuse = light * diff * sample.rgb;
					float3 specular = light * sample.rgb * spec * _SpecularIntensity;
					//float3 specular = light * float3(1.0f,1.0f,1.0f) * spec * _SpecularIntensity;
					
					float4 res = float4(float3(ambient + diffuse + specular),1.0f);
					//float4 res = float4(float3(ambient + diffuse), 1.0f);
					res.a = _Transparency;
					return res;
				}
				ENDCG
			}
			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
		}
}
