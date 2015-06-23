#include "HLSLSupport.cginc"
#include "UnityShaderVariables.cginc"
#define UNITY_PASS_FORWARDBASE
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "MMD4Mecanim-MMDLit-AutoLight.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal

#include "MMD4Mecanim-MMDLit-Lighting.cginc"
#include "MMD4Mecanim-MMDLit-SurfaceEdge-Lighting.cginc"

struct v2f_surf {
  float4 pos : SV_POSITION;
  half3 vlight : TEXCOORD1;
  half3 viewDir : TEXCOORD2;
  LIGHTING_COORDS(3,4)
};

v2f_surf vert_surf (appdata_full v)
{
	v2f_surf o;
	v.vertex = MMDLit_GetEdgeVertex(v.vertex, v.normal);
	o.pos = MMDLit_TransformEdgeVertex(v.vertex);
	o.viewDir = (half3)WorldSpaceViewDir(v.vertex);
	float3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);
	o.vlight = ShadeSH9(float4(worldN, 1.0));
	#ifdef VERTEXLIGHT_ON
	// Skip Vertex Lighting for Edge
	//float3 worldPos = mul(_Object2World, v.vertex).xyz;
	//o.vlight += Shade4PointLights(
	//	unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
	//	unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
	//	unity_4LightAtten0, worldPos, worldN);
	#endif // VERTEXLIGHT_ON
	TRANSFER_VERTEX_TO_FRAGMENT(o);
	return o;
}

fixed4 frag_surf (v2f_surf IN) : COLOR
{
	half alpha;
	half3 albedo = MMDLit_GetAlbedo(alpha);

	// Warning: Maybe crash Android / Adreano 205
	clip(alpha - (0.1 / 255.0)); // Like as MMD
	
	half atten = LIGHT_ATTENUATION(IN);
	half3 c = 0;

	c = MMDLit_Lighting(albedo, atten, IN.vlight);
	c += albedo * IN.vlight;
	return fixed4(c, alpha);
}
