#include "HLSLSupport.cginc"
#include "UnityShaderVariables.cginc"
#define UNITY_PASS_FORWARDADD
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal

#include "MMD4Mecanim-MMDLit-Surface-Lighting.cginc"

struct v2f_surf
{
	float4 pos : SV_POSITION;
	float4 pack0 : TEXCOORD0;
	half3 normal : TEXCOORD1;
	half4 lightDir : TEXCOORD2;
	half4 viewDir : TEXCOORD3;
	LIGHTING_COORDS(4,5)
	half3 mmd_uvwSphere : TEXCOORD6;
	half3 mmd_globalAmbient : TEXCOORD7;
};

float4 _MainTex_ST;

v2f_surf vert_surf(appdata_full v)
{
	v2f_surf o;
	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
	o.normal = mul((float3x3)_Object2World, SCALED_NORMAL);
	#ifdef SPHEREMAP_OFF
	o.mmd_uvwSphere = 0;
	#else
	half3 norm = normalize(mul((float3x3)UNITY_MATRIX_MV, v.normal));
	half3 eye = normalize(mul(UNITY_MATRIX_MV, v.vertex).xyz);
	o.mmd_uvwSphere = reflect(eye, norm);
	#endif
	o.mmd_globalAmbient = ShadeSH9(float4(o.normal, 1.0)) * _AmbientRate;
	o.lightDir.xyz = WorldSpaceLightDir(v.vertex);
	o.viewDir.xyz = WorldSpaceViewDir(v.vertex);
	half NdotL = dot(o.normal, o.lightDir);
	half toonRefl = MMDLit_GetToolRefl(NdotL);
	o.pack0.z = toonRefl;
	o.pack0.w = MMDLit_GetForwardAddStr(toonRefl);
	o.lightDir.w = 0;
	o.viewDir.w = MMDLit_GetToonShadow(toonRefl);
	TRANSFER_VERTEX_TO_FRAGMENT(o);
	return o;
}

inline half3 frag_core(in v2f_surf IN, half4 albedo)
{
	half atten = LIGHT_ATTENUATION(IN);
	#ifndef USING_DIRECTIONAL_LIGHT
	half3 lightDir = normalize((half3)IN.lightDir);
	#else
	half3 lightDir = (half3)IN.lightDir;
	#endif

	half toonRefl = (half)IN.pack0.z;
	half forwardAddStr = (half)IN.pack0.w;
	half toonShadow = IN.viewDir.w;
	half3 c = MMDLit_Lighting_Add(
		(half3)albedo,
		IN.mmd_uvwSphere,
		toonRefl,
		toonShadow,
		IN.normal,
		(half3)lightDir,
		normalize((half3)IN.viewDir),
		atten,
		IN.mmd_globalAmbient);

	c *= forwardAddStr;
	return c;
}

fixed4 frag_surf(v2f_surf IN) : COLOR
{
	half4 albedo = MMDLit_GetAlbedo(IN.pack0.xy);
	albedo.a *= _Color.a; // for Transparency
	
	// Warning: Maybe crash Android / Adreano 205
	clip(albedo.a - (0.1 / 255.0)); // Like as MMD
	
	half3 c = frag_core(IN, albedo);
	c = min(c, 1.0);
	c *= albedo.a;
	return fixed4(c, 0.0);
}

fixed4 frag_fast(v2f_surf IN) : COLOR
{
	half3 c = frag_core(IN, MMDLit_GetAlbedo(IN.pack0.xy));
	return fixed4(c, 0.0);
}
