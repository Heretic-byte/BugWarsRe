﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Particles/Additive" {
	Properties{
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Particle Texture", 2D) = "white" {}
		_InvFade("Soft Particles Factor", Range(0.01,3.0)) = 1.0
			_ScrollSpeeds("Scroll Speeds", vector) = (-5, -20, 0, 0)
	}

		Category{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			Blend SrcAlpha One
			AlphaTest Greater .01
			ColorMask RGB
			Cull Off Lighting Off ZWrite Off Fog { Color(0,0,0,0) }
			BindChannels {
				Bind "Color", color
				Bind "Vertex", vertex
				Bind "TexCoord", texcoord
			}

			// ---- Fragment program cards
			SubShader {
				Pass {

					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma fragmentoption ARB_precision_hint_fastest
					#pragma multi_compile_particles

					#include "UnityCG.cginc"

					sampler2D _MainTex;
					fixed4 _TintColor;

					struct appdata_t {
						float4 vertex : POSITION;
						fixed4 color : COLOR;
						float2 texcoord : TEXCOORD0;
						float2 uv : TEXCOORD0;
					};

					struct v2f {
						float4 vertex : POSITION;
						fixed4 color : COLOR;
						float2 texcoord : TEXCOORD0;
						float2 uv : TEXCOORD0;
						
						UNITY_FOG_COORDS(1)

						#ifdef SOFTPARTICLES_ON
						float4 projPos : TEXCOORD1;
						#endif
					};

					float4 _MainTex_ST;
					float4 _ScrollSpeeds;
					v2f vert(appdata_t v)
					{
						v2f o;
						o.vertex = UnityObjectToClipPos(v.vertex);
					
						o.color = v.color;
						
						o.texcoord = TRANSFORM_TEX(v.uv, _MainTex);
						o.texcoord += _ScrollSpeeds * _Time.x;
						UNITY_TRANSFER_FOG(o, o.vertex);
						
						return o;
					}

					sampler2D _CameraDepthTexture;
					float _InvFade;

				
					fixed4 frag(v2f i) : COLOR
					{
					
					return 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord);
					}
					ENDCG
				}
			}

			// ---- Dual texture cards
			SubShader {
				Pass {
					SetTexture[_MainTex] {
						constantColor[_TintColor]
						combine constant * primary
					}
					SetTexture[_MainTex] {
						combine texture * previous DOUBLE
					}
				}
			}

						// ---- Single texture cards (does not do color tint)
						SubShader {
							Pass {
								SetTexture[_MainTex] {
									combine texture * primary
								}
							}
						}
		}
}