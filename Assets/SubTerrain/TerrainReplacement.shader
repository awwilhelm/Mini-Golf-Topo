Shader "Hidden/TerrainEngine/Splatmap/Lightmap-FirstPass" {
Properties {
	_Control ("Control (RGBA)", 2D) = "red" {}
	_Splat3 ("Layer 3 (A)", 2D) = "white" {}
	_Splat2 ("Layer 2 (B)", 2D) = "white" {}
	_Splat1 ("Layer 1 (G)", 2D) = "white" {}
	_Splat0 ("Layer 0 (R)", 2D) = "white" {}
	// used in fallback on old cards
	_MainTex ("BaseMap (RGBA)", 2D) = "white" {}
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (1, 1, 1, 1)

}

SubShader {
	Tags {
		"SplatCount" = "4"
		"Queue" = "Geometry-100"
		"RenderType" = "Opaque"
	}
CGPROGRAM
#pragma surface surf BlinnPhong vertex:vert
#pragma target 3.0
#include "UnityCG.cginc"

struct Input {
	float3 worldPos;
	float2 uv_Control : TEXCOORD0;
	float2 uv_Splat0 : TEXCOORD1;
	float2 uv_Splat1 : TEXCOORD2;
	float2 uv_Splat2 : TEXCOORD3;
	float2 uv_Splat3 : TEXCOORD4;
};

// Supply the shader with tangents for the terrain
void vert (inout appdata_full v) {

	// A general tangent estimation
	float3 T1 = float3(1, 0, 1);
	float3 Bi = cross(T1, v.normal);
	float3 newTangent = cross(v.normal, Bi);

	normalize(newTangent);

	v.tangent.xyz = newTangent.xyz;

	if (dot(cross(v.normal,newTangent),Bi) < 0)
		v.tangent.w = -1.0f;
	else
		v.tangent.w = 1.0f;
}

sampler2D _Control;
sampler2D _BumpMap0, _BumpMap1, _BumpMap2, _BumpMap3;
sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
float _Spec0,_Spec1,_Spec2,_Spec3,_MixScale,_Mix0,_Mix1,_Mix2,_Mix3;

void surf (Input IN, inout SurfaceOutput o) {

	half4 splat_control = tex2D (_Control, IN.uv_Control);
	half3 col;
	half4 splat0 = tex2D (_Splat0, IN.uv_Splat0);
	half4 splat1 = tex2D (_Splat1, IN.uv_Splat1);
	half4 splat2 = tex2D (_Splat2, IN.uv_Splat2);
	half4 splat3 = tex2D (_Splat3, IN.uv_Splat3);
	// 4 splats, normals, and specular settings

	////// Texture Blending - Comment these lines instead of the others if you don't need it
	////// Only the first layer has a Mixed Normal Map
	col = (splat_control.r * lerp(splat0.rgb,(tex2D (_Splat0, IN.uv_Splat0 * -_MixScale).rgb), _Mix0));
	o.Normal = (splat_control.r * lerp(UnpackNormal(tex2D(_BumpMap0, IN.uv_Splat0)),UnpackNormal(tex2D(_BumpMap0, IN.uv_Splat0 * -_MixScale)), _Mix0));
	//////

	//col += splat_control.r * splat0.rgb;
	//o.Normal = splat_control.r * UnpackNormal(tex2D(_BumpMap0, IN.uv_Splat0));
	o.Gloss = splat0.a * splat_control.r * _Spec0;
	o.Specular = 0.3 * splat_control.r;

	col += (splat_control.g * lerp(splat1.rgb,(tex2D (_Splat1, IN.uv_Splat1 * -_MixScale).rgb), _Mix1));

	//col += splat_control.g * splat1.rgb;
	o.Normal += splat_control.g * UnpackNormal(tex2D(_BumpMap1, IN.uv_Splat1));
	o.Gloss += splat1.a * splat_control.g * _Spec1;
	o.Specular += 0.3 * splat_control.g;

	col += (splat_control.b * lerp(splat2.rgb,(tex2D (_Splat2, IN.uv_Splat2 * -_MixScale).rgb), _Mix2));

	//col += splat_control.b * splat2.rgb;
	o.Normal += splat_control.b * UnpackNormal(tex2D(_BumpMap2, IN.uv_Splat2));
	o.Gloss += splat2.a * splat_control.b * _Spec2;
	o.Specular += 0.3 * splat_control.b;

	col += (splat_control.a * lerp(splat3.rgb,(tex2D (_Splat3, IN.uv_Splat3 * -_MixScale).rgb), _Mix3));

	//col += splat_control.a * splat3.rgb;
	o.Normal += splat_control.a * UnpackNormal(tex2D(_BumpMap3, IN.uv_Splat3));
	o.Gloss += splat3.a * splat_control.a * _Spec3;
	o.Specular += 0.3 * splat_control.a;

	o.Albedo = col;
	o.Alpha = 0.0;
}
ENDCG
}

// Fallback to Diffuse
Fallback "Diffuse"
}