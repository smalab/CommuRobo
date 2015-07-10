#include "MMD4Mecanim-MMDLit-Lighting.cginc"

half4 _Color;
half4 _Specular;
half4 _Ambient;
half _Shininess;
half _ShadowLum;
half _AmbientRate;
half _AmbientToDiffuse;

sampler2D _MainTex;
sampler2D _ToonTex;

half _AddLightToonCen;
half _AddLightToonMin;

half4 _Emissive;

samplerCUBE _SphereCube;

#define MMDLIT_GLOBALLIGHTING		half3(0.6, 0.6, 0.6)
#define MMDLIT_CENTERAMBIENT		half3(0.5, 0.5, 0.5)
#define MMDLIT_CENTERAMBIENT_INV	half3(1.0 / 0.5, 1.0 / 0.5, 1.0 / 0.5)

// Ambient Feedback Rate from Unity.
inline half3 MMDLit_GetTempAmbientL()
{
	return max(MMDLIT_CENTERAMBIENT - (half3)_Ambient, half3(0,0,0)) * MMDLIT_CENTERAMBIENT_INV;
}

inline half3 MMDLit_GetTempAmbient( half3 globalAmbient )
{
	return globalAmbient * (half3(1,1,1) - MMDLit_GetTempAmbientL());
}

inline half3 MMDLit_GetTempDiffuse( half3 globalAmbient )
{
	half3 tempColor = min((half3)_Ambient + (half3)_Color * MMDLIT_GLOBALLIGHTING, half3(1,1,1));
	tempColor = max(tempColor - MMDLit_GetTempAmbient(globalAmbient), half3(0,0,0));
	#ifdef AMB2DIFF_ON
	tempColor *= min(globalAmbient * _AmbientToDiffuse, half3(1,1,1)); // Feedback ambient for Unity5.
	#endif
	return tempColor;
}

inline void MMDLit_GetBaseColor(
	half3 albedo,
	half3 tempDiffuse,
	half3 uvw_Sphere,
	out half3 baseC,
	out half3 baseD)
{
	#ifdef SPHEREMAP_MUL
	half3 sph = (half3)texCUBE(_SphereCube, uvw_Sphere);
	baseC = albedo * sph;
	baseD = baseC * tempDiffuse; // for Diffuse only.
	#elif SPHEREMAP_ADD
	half3 sph = (half3)texCUBE(_SphereCube, uvw_Sphere);
	baseC = albedo + sph;
	baseD = albedo * tempDiffuse + sph; // for Diffuse only.
	#else
	baseC = albedo;
	baseD = albedo * tempDiffuse;
	#endif
}

inline half4 MMDLit_GetAlbedo(float2 uv_MainTex)
{
	return (half4)tex2D(_MainTex, uv_MainTex);
}

inline half MMDLit_GetToolRefl(half NdotL)
{
	return NdotL * 0.5 + 0.5;
}

inline half MMDLit_GetToonShadow(half toonRefl)
{
	half toonShadow = toonRefl * 2.0;
	return (half)saturate(toonShadow * toonShadow - 1.0);
}

inline half MMDLit_GetForwardAddStr(half toonRefl)
{
	half toonShadow = (toonRefl - _AddLightToonCen) * 2.0;
	return (half)clamp(toonShadow * toonShadow - 1.0, _AddLightToonMin, 1.0);
}

// for ForwardBase
inline half3 MMDLit_GetRamp(half NdotL, half shadowAtten)
{
	half refl = (NdotL * 0.5 + 0.5) * shadowAtten;
	half toonRefl = refl;

	#ifdef SELFSHADOW_ON
	refl = 0;
	#endif
	
	half3 ramp = (half3)tex2D(_ToonTex, half2(refl, refl));

	#ifdef SELFSHADOW_ON
	half toonShadow = MMDLit_GetToonShadow(toonRefl);
	half3 rampSS = (1.0 - toonShadow) * ramp + toonShadow;
	ramp = rampSS;
	#endif

	ramp = saturate(1.0 - (1.0 - ramp) * _ShadowLum);
	return ramp;
}

// for ForwardAdd
inline half3 MMDLit_GetRamp_Add(half toonRefl, half toonShadow)
{
	half refl = toonRefl;
	
	#ifdef SELFSHADOW_ON
	refl = 0;
	#endif
	
	half3 ramp = (half3)tex2D(_ToonTex, half2(refl, refl));

	#ifdef SELFSHADOW_ON
	half3 rampSS = (1.0 - toonShadow) * ramp + toonShadow;
	ramp = rampSS;
	#endif
	
	ramp = saturate(1.0 - (1.0 - ramp) * _ShadowLum);
	return ramp;
}

// for FORWARD_BASE
inline half3 MMDLit_Lighting(
	half3 albedo,
	half3 uvw_Sphere,
	half NdotL,
	half3 normal,
	half3 lightDir,
	half3 viewDir,
	half atten,
	half shadowAtten,
	out half3 baseC,
	half3 globalAmbient)
{
	half3 ramp = MMDLit_GetRamp(NdotL, shadowAtten);
	half3 lightColor = (half3)_LightColor0 * MMDLIT_ATTEN(atten);

	half3 baseD;
	MMDLit_GetBaseColor(albedo, MMDLit_GetTempDiffuse(globalAmbient), uvw_Sphere, baseC, baseD);
	
	half3 c = baseD * lightColor * ramp;
	
	#ifdef SPECULAR_ON
	half refl = MMDLit_SpecularRefl(normal, lightDir, viewDir, _Shininess);
	c += (half3)_Specular * lightColor * refl;
	#endif

	#ifdef EMISSIVE_ON
	// AutoLuminous
	c += baseC * (half3)_Emissive;
	#endif
	return c;
}

// for FORWARD_ADD
inline half3 MMDLit_Lighting_Add(
	half3 albedo,
	half3 uvw_Sphere,
	half toonRefl,
	half toonShadow,
	half3 normal,
	half3 lightDir,
	half3 viewDir,
	half atten,
	half3 globalAmbient)
{
	half3 ramp = MMDLit_GetRamp_Add(toonRefl, toonShadow);
	half3 lightColor = (half3)_LightColor0 * MMDLIT_ATTEN(atten);

	half3 baseC;
	half3 baseD;
	MMDLit_GetBaseColor(albedo, MMDLit_GetTempDiffuse(globalAmbient), uvw_Sphere, baseC, baseD);

	half3 c = baseD * lightColor * ramp;

	#ifdef SPECULAR_ON
	half refl = MMDLit_SpecularRefl(normal, lightDir, viewDir, _Shininess);
	c += (half3)_Specular * lightColor * refl;
	#endif
	
	#ifdef EMISSIVE_ON
	// AutoLuminous
	c += baseC * (half3)_Emissive;
	#endif
	return c;
}

inline half MMDLit_MulAtten(half atten, half shadowAtten)
{
	return atten * shadowAtten;
}
