Shader "Shaders/WaterBell"
{
	Properties
	{
		[Header(Shading)]
		_MainTex("Main Texture", 2D) = "white" {}
		_AlphaMap("Alpha Texture", 2D) = "white" {}
		_Transparency("Transparency",Range(0,1)) = 1.0
		_Amplitude("Amplitude",Range(0,10)) = 0.25
		_Lerp_reflect("Reflect Lerp",Range(0,1)) = 0.1

		_WindStrength("Wind Strength",Float) = 1
	}

		CGINCLUDE
		#include "UnityCG.cginc"
		#include "Autolight.cginc"
		
		float _WindStrength;
		float _Amplitude;
		
		float _Transparency;
		float _Lerp_reflect;
		
		sampler2D _MainTex;
		float4 _MainTex_ST;

		sampler2D _AlphaMap;
		float4 _AlphaMap_ST;
		
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
			};

		
		vertexOutput vert(vertexInput v)
		{
			vertexOutput o;

			o.vertex = v.vertex;
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			
			o.worldNormal = v.normal;
			o.viewDir = WorldSpaceViewDir(v.vertex);

			o.tangent = v.tangent;
			o.id = v.id;
			return o;
		}

		
		[maxvertexcount(3)]
		void geo(triangle vertexOutput IN[3], inout TriangleStream<vertexOutput> triStream) {
			float4 pos;

			for (int i = 0; i < 3; i++) {
				pos = IN[i].vertex;
				pos.y += cos(_WindStrength*(_Time.y + IN[i].id))*_Amplitude + _Amplitude;
				IN[i].vertex = UnityObjectToClipPos(pos);
				
				IN[i].worldNormal = UnityObjectToWorldNormal(IN[0].worldNormal);
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
				//pour être afficher a la fin
				ZWrite off
				
				CGPROGRAM
				#pragma vertex vert
				#pragma geometry geo
				#pragma fragment frag
				#pragma target 4.6

				#include "Lighting.cginc"
				

				float4 frag(vertexOutput i) : SV_Target{
					float2 coords = float2(_MainTex_ST.x * (i.uv.x + _MainTex_ST.z),_MainTex_ST.y * (i.uv.y + _MainTex_ST.w));
					float4 sample = tex2D(_MainTex, coords); 
					coords = float2(_AlphaMap_ST.x * (i.uv.x + _AlphaMap_ST.z), _AlphaMap_ST.y * (i.uv.y + _AlphaMap_ST.w));
					float4 alpha = tex2D(_AlphaMap,coords);

					if (alpha.a != 1.0f) {
						discard;
					}

					float3 viewDir = normalize(i.viewDir);

					float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
					float3 normal = normalize(i.worldNormal);
					
					//calcul reflection
					float3 I = viewDir;
					float3 R = reflect(-I, normal);
					half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, R);
					half3 skyColor = DecodeHDR(skyData, unity_SpecCube0_HDR);
					float4 reflect = float4(skyColor.rgb, 1.0f);
					
					//calcul refraction
					//air sur eau
					float ratio = 1.0f / 1.33f;
					R = refract(I, normal,ratio);
					skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, R);
					skyColor = DecodeHDR(skyData, unity_SpecCube0_HDR);
					float4 refract = float4(skyColor.rgb,1.0f);

					float4 light = _LightColor0;

					float4 res = sample;
					res = lerp(res,reflect,_Lerp_reflect);
					//res *= light;
					res.a = _Transparency;
					return res;
				}
				ENDCG
			}
		}
}
