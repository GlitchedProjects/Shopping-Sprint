
HEADER
{
	Description = "";
}

FEATURES
{
	#include "common/features.hlsl"
}

MODES
{
	Forward();
	Depth();
	ToolsShadingComplexity( "tools_shading_complexity.shader" );
}

COMMON
{
	#ifndef S_ALPHA_TEST
	#define S_ALPHA_TEST 0
	#endif
	#ifndef S_TRANSLUCENT
	#define S_TRANSLUCENT 1
	#endif
	
	#include "common/shared.hlsl"
	#include "procedural.hlsl"

	#define S_UV2 1
}

struct VertexInput
{
	#include "common/vertexinput.hlsl"
	float4 vColor : COLOR0 < Semantic( Color ); >;
};

struct PixelInput
{
	#include "common/pixelinput.hlsl"
	float3 vPositionOs : TEXCOORD14;
	float3 vNormalOs : TEXCOORD15;
	float4 vTangentUOs_flTangentVSign : TANGENT	< Semantic( TangentU_SignV ); >;
	float4 vColor : COLOR0;
	float4 vTintColor : COLOR1;
	#if ( PROGRAM == VFX_PROGRAM_PS )
		bool vFrontFacing : SV_IsFrontFace;
	#endif
};

VS
{
	#include "common/vertex.hlsl"

	PixelInput MainVs( VertexInput v )
	{
		
		PixelInput i;
		i.vPositionPs = float4(v.vPositionOs.xy, 0.0f, 1.0f );
		i.vPositionWs = float3(v.vTexCoord, 0.0f);
		
		return i;
		
	}
}

PS
{
	#include "common/pixel.hlsl"
	#include "postprocess/functions.hlsl"
	#include "postprocess/common.hlsl"
	RenderState( CullMode, F_RENDER_BACKFACES ? NONE : DEFAULT );
		
	SamplerState g_sSampler0 < Filter( ANISO ); AddressU( WRAP ); AddressV( WRAP ); >;
	Texture2D g_tColorBuffer < Attribute( "ColorBuffer" ); SrgbRead ( true ); >;
	CreateInputTexture2D( Texture_ps_0, Srgb, 8, "None", "_color", ",0/,0/0", DefaultFile( "materials/tools/handle_edged_circle.tga" ) );
	Texture2D g_tTexture_ps_0 < Channel( RGBA, Box( Texture_ps_0 ), Srgb ); OutputFormat( DXT5 ); SrgbRead( True ); >;
	
	float4 MainPs( PixelInput i ) : SV_Target0
	{

		
		float2 l_0 = CalculateViewportUv( i.vPositionSs.xy );
		float2 l_1 = float2( 0, 0 );
		float2 l_2 = l_0 + l_1;
		float4 l_3 = Tex2DS( g_tTexture_ps_0, g_sSampler0, l_2 );
		float4 l_4 = float4( 1, 1, 1, 1 );
		float4 l_5 = l_3 * l_4;
		float4 l_6 = saturate( ( l_5 - float4( 0.10657579, 0.10657579, 0.10657579, 0.10657579 ) ) / ( float4( 0.51473904, 0.51473904, 0.51473904, 0.51473904 ) - float4( 0.10657579, 0.10657579, 0.10657579, 0.10657579 ) ) ) * ( float4( 0, 0, 0, 0 ) - float4( 0.7273557, 0.7273557, 0.7273557, 0.7273557 ) ) + float4( 0.7273557, 0.7273557, 0.7273557, 0.7273557 );
		

		return float4( l_5.xyz, l_6.x );
	}
}
