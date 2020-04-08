// Our texture sampler

// Size of the image
float xSize;
float ySize;

float xDraw;
float yDraw;

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
	
	// Getting the size of 1 pixel of the image, basically pixel proportion
	float vertPixSize = 1.0f/ySize; 
	float horPixSize = 1.0f/xSize;
	

	// Declaring color
	float4 color;

	// Checking if what we are trying to draw x size as devided by what x size actually is, is less then 0.6 (if we are trying to draw it in less then 60% of what its original size is), same for y, then start doing some ani alising
	if(xDraw/xSize < .6f || yDraw/ySize < .6f)
	{
		// If we are trying to draw it in less then 40%, then go over 2 pixels instad of 1
		if(xDraw/xSize < .4f || yDraw/ySize < .4f)
		{
			vertPixSize = 2.0f/ySize;
			horPixSize = 2.0f/xSize;
		}

		// Testing the pixel that its currently passing through with the cordinate but also adding our pixel sizes that we got above                         5 times the processing of every pixel! may be hard on some gpu;s
		float4 aboveColor = tex2D(TextureSampler, (input.TextureCordinate) + float2(0, -vertPixSize)); // Getting the pixel thats next to it (above)
		
		float4 belowColor = tex2D(TextureSampler, (input.TextureCordinate) + float2(0, vertPixSize)); // Getting the pixel thats next to it (below)
		
		float4 leftColor = tex2D(TextureSampler, (input.TextureCordinate) + float2(-horPixSize, 0)); // Getting the pixel thats next to it (left)
		
		float4 rightColor = tex2D(TextureSampler, (input.TextureCordinate) + float2(horPixSize, 0)); // Getting the pixel thats next to it (right)
		
		//float greyscaleAverage = (texColor.r + texColor.g + texColor.b)/3;
		
		// Doing the averaging basically taking the pixel (color) we have, and its above, below, left and right color and deviding it by 5 (averaging the color of the pixel and around it), doing it to RGB, Alpha
		 color = float4((texColor.r + aboveColor.r + belowColor.r + leftColor.r + rightColor.r)/5, // R
		 (texColor.g + aboveColor.g + belowColor.g + leftColor.g + rightColor.g)/5, // G
		 (texColor.b + aboveColor.b + belowColor.b + leftColor.b + rightColor.b)/5, // B
		 (texColor.a + aboveColor.a + belowColor.a + leftColor.a + rightColor.a)/5); // Alphas
	}
	else
	{
		color = float4(texColor.r, texColor.g, texColor.b, texColor.a);
	}
	
	

	return color * filterColor;
}

// Compile our shader
technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
    }
}
