﻿#includeres "Ultraviolet.OpenGL.Resources.SkinnedEffectPreamble.glsl" executing

 in  vec4 uv_Position0;
 in  vec3 uv_Normal0;
 in  vec2 uv_TextureCoordinate0;
 in ivec4 uv_BlendIndices0;
 in  vec4 uv_BlendWeights0;

out vec2 vTexCoord;
out vec4 vPositionWS;
out vec3 vNormalWS;
out vec4 vDiffuse;
out vec4 vPositionPS;

void main()
{
	vec4 position = uv_Position0;
	vec3 normal = uv_Normal0;

    Skin(position, normal, uv_BlendIndices0, uv_BlendWeights0, 2);

	CommonVSOutputPixelLighting  cout = ComputeCommonVSOutputPixelLighting(uv_Position0, uv_Normal0);
	SetCommonVSOutputParamsPixelLighting;
	
	vDiffuse = vec4(1.0, 1.0, 1.0, DiffuseColor.a);
	vTexCoord = FlipTextureCoordinates(uv_TextureCoordinate0);
}