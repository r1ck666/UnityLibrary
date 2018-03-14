Shader "Custom/CurveRoadShader_LowQuaility"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

        _Length ("MeshLength", Float) = 0
        _P0 ("Point 0", Vector) = (0, 0, 0, 0)
        _P1 ("Point 1", Vector) = (0, 0, 0, 0)
        _P2 ("Point 2", Vector) = (0, 0, 0, 0)
        _P3 ("Point 3", Vector) = (0, 0, 0, 0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

            // Add
            float4 _P0;
            float4 _P1;
            float4 _P2;
            float4 _P3;
            float _Length;
            fixed4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;

                // BazierLine
                float t = v.vertex.z / _Length;
                float oneMinusT = 1.0f - t;
                float4 b = oneMinusT * oneMinusT * oneMinusT * _P0 + 
                    3.0f * oneMinusT * oneMinusT * t * _P1 + 
                    3.0f * oneMinusT * t * t * _P2 + 
                    t * t * t * _P3 +
                    float4 (v.vertex.x, 0, 0, 0);

				o.vertex = UnityObjectToClipPos(b);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
