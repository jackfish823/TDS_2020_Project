// Our texture sampler

// Size of the image
float sinLoc;


float4 filterColor;


texture Texture;
sampler TextureSampler = sampler_state
{
    Texture = <Texture>;
};

// This data comes from the sprite batch vertex shader ^^^^in big ani alising is loosig information bla bla ep13 explainded^^^^^^^^, explain how then say basically editing the picture to not be correct but visually it becomes closer to what ur expexting
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCordinate : TEXCOORD0;
};

// Our pixel shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR
{
	// Testing the pixel that its currently passing through for the color with the cordinate
	float4 texColor = tex2D(TextureSampler, input.TextureCordinate);

	// Declaring color
	float4 color;

	if (texColor.a != 0)
	{
		color = float4(texColor.r + (texColor.r - filterColor.r) * sinLoc, texColor.g + (texColor.g - filterColor.g) * sinLoc, texColor.b + (texColor.b - filterColor.b) * sinLoc, texColor.a);
	}
	else
	{
		color = float4(texColor.r, texColor.g, texColor.b, texColor.a);
	}
	

	return color * filterColor * input.Color;
}

// Compile our shader
technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
    }
}
