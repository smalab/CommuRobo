#include "HLSLSupport.cginc"
#include "UnityShaderVariables.cginc"
#define UNITY_PASS_FORWARDBASE
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "MMD4Mecanim-MMDLit-AutoLight.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal

#include "MMD4Mecanim-MMDLit-Surface-Lighting.cginc"

struct v2f_surf {
	float4 pos : SV_POSITION;
	float2 pack0 : TEXCOORD0;
	half3 normal : TEXCOORD1;
	half3 vlight : TEXCOORD2;
	half3 viewDir : TEXCOORD3;
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
	float3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);
	o.normal = worldN;

	#ifdef SPHEREMAP_OFF
	o.mmd_uvwSphere = 0;
	#else
	half3 norm = normalize(mul((float3x3)UNITY_MATRIX_MV, v.normal));
	half3 eye = normalize(mul(UNITY_MATRIX_MV, v.vertex).xyz);
	o.mmd_uvwSphere = reflect(eye, norm);
	#endif
	
	o.viewDir = (half3)WorldSpaceViewDir(v.vertex);
	
	o.vlight = ShadeSH9(float4(worldN, 1.0)) * _AmbientRate;
	o.mmd_globalAmbient = o.vlight;
	#ifdef VERTEXLIGHT_ON
	float3 worldPos = mul(_Object2World, v.vertex).xyz;
	o.vlight += Shade4PointLights(
		unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
		unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
		unity_4LightAtten0, worldPos, worldN ) * _AmbientRate;
	#endif // VERTEXLIGHT_ON
	// Feedback Ambient.
	o.vlight *= (half3)1.0 - MMDLit_GetTempAmbientL();

	TRANSFER_VERTEX_TO_FRAGMENT(o);
	return o;
}

inline half4 frag_core(in v2f_surf IN, half4 albedo)
{
	half atten = LIGHT_ATTENUATION(IN);
	half shadowAtten = SHADOW_ATTENUATION(IN);
	half3 c;

	half3 baseC;
	half NdotL = dot(IN.normal, _WorldSpaceLightPos0.xyz);
	c = MMDLit_Lighting(
		(half3)albedo,
		IN.mmd_uvwSphere,
		NdotL,
		IN.normal,
		_WorldSpaceLightPos0.xyz,
		normalize(IN.viewDir),
		atten,
		shadowAtten,
		baseC,
		IN.mmd_globalAmbient);

	c += baseC * IN.vlight;

	return half4(c, albedo.a);
}

// for Transparency
half4 frag_surf(v2f_surf IN) : COLOR
{
	half4 albedo = MMDLit_GetAlbedo(IN.pack0.xy);
	albedo.a *= _Color.a; // for Transparency
	
	// Warning: Maybe crash Android / Adreano 205
	clip(albedo.a - (0.1 / 255.0)); // Like as MMD

	return frag_core(IN, albedo);
}

// for Opaque
half4 frag_fast(v2f_surf IN) : COLOR
{
	half4 albedo = MMDLit_GetAlbedo(IN.pack0.xy);
	return frag_core(IN, albedo);
}
