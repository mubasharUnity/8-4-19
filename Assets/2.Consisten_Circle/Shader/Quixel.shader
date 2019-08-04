Shader "Custom/Quixel"
{
	Properties
	{
		_ForegroundColor("Foreground Color", Color) = (1,1,1,1)
		//these are normalized value so they can be adjested with screen sizes
		_WidthOfArc("Width of Arc", Range(0,1)) = 0.1
		_InternalRadius("Internal Radius", Range(0,1)) = 0.5
	}
	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			fixed4  _ForegroundColor;
			float _InternalRadius;
			float _WidthOfArc;
			float _Angle;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{				
				float u = (i.uv.x-0.5) * 2;//shift bounds from 0 to 1 to -1 to 1. reposition (0,0) in quad's center
				float v = (i.uv.y-0.5) * 2;				

				fixed4 col = fixed4(0,0,0,0);
				fixed radius = sqrt(u*u + v*v);//radius is from quad's center and in range 0-1
				
				half value = radius > _InternalRadius &&
					radius < _InternalRadius + _WidthOfArc ;//true = 1 and false = 0

				col = _ForegroundColor * value;
				clip(col.a - 0.01);

				return col;
			}
			ENDCG
		}
	}
}
