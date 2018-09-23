Shader "Custom/HighlightBordersShader"
{
	Properties
	{
		_Color("Main Color", Color) = (48, 255, 32, 255)
		_OutlineColor("Outline color", Color) = (229, 35, 27, 255)
		_OutlineWidth("Outline width", Range(1.0, 5.0)) = 1.2
	}

	CGINCLUDE
	struct vertexInput {
		float4 vertex : POSITION;
	};

	struct vertexOutput {
		float4 pos : SV_POSITION;
	};
	ENDCG

	SubShader
	{
		Pass
		{
			ZWrite OFF

			CGPROGRAM
			float4 _OutlineColor;
			float _OutlineWidth;

			#pragma vertex vert
			#pragma fragment frag

			vertexOutput vert(vertexInput v) {
				v.vertex.xyz *= _OutlineWidth;

				vertexOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			half4 frag(vertexOutput i) : COLOR{
				return _OutlineColor;
			}	
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4 _Color;

			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(vertexOutput i) : COLOR
			{
				return _Color;
			}
			ENDCG
		}
	}

	Fallback  "Diffuse"
}
