Shader "Custom/TVShader" {
	Properties {

		_MainTex("Texture (RGB)", 2D) = "white" {}
		_Sandstorm("Sandstorm", Range(0,1)) = 0
		_Noise("Noise",Range(0,1)) = 0
		_Distortion("Distortion",float) = 0
		_Alpha("Alpha",float) = 1
		_Contrast("_Contrast",Range(0,20)) = 0
		_Brightness("Brightness",Range(0,20)) = 1
	}
		SubShader
		{
			//カーリングオフ、
			Cull Off
			//実線で描画する場合On 半透明効果使う場合Off
			ZWrite Off
			//常に全面
			ZTest Always
			//アルファーブレンドon
			Blend SrcAlpha OneMinusSrcAlpha
			Pass
		{
			CGPROGRAM
			// 頂点シェーダー
#pragma vertex vert
			// フラグメントシェーダー
#pragma fragment frag
			// Targetレベル 
			// 詳しくは「https://docs.unity3d.com/jp/530/Manual/SL-ShaderCompileTargets.html」
#pragma target 3.0
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



		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = v.uv;
			return o;
		}
		float rand(float2 co) {
			return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
		}
		float2 mod(float2 a, float2 b)
		{
			return a - floor(a / b) * b;
		}

		sampler2D _MainTex;
		float _Noise;
		float _Sandstorm;
		float _Distortion;
		float _Alpha;
		float _Contrast;
		float _Brightness;

		fixed4 frag(v2f i) : SV_Target
		{
			float2 inUV = i.uv;
			float2 uv = i.uv - 0.5;

			// UV座標を再計算し、画面を歪ませる
			float vignet = length(uv);
			uv /= 1 - vignet * _Distortion;
			float2 texUV = uv + 0.5;

			// 画面外なら描画しない
			if (max(abs(uv.y) - 0.5, abs(uv.x) - 0.5) > 0)
			{
				return float4(0, 0, 0, 1);
			}

			// 色を計算
			float3 col;

			texUV.x += (rand(floor(texUV.y * 500) + _Time.y) - 0.5) * _Noise;
			// 色を取得、RGBを少しずつずらす
			col.r = tex2D(_MainTex, texUV).r;
			col.g = tex2D(_MainTex, texUV - float2(0.002, 0)).g;
			col.b = tex2D(_MainTex, texUV - float2(0.004, 0)).b;

			//コントラスト
			//float4 c = tex2D(_MainTex, i.uv);
			//c = 1 / (1 + _Contrast);
			//col.rgb += c;
			// RGBノイズ
			if (rand((rand(floor(texUV.y * 500) + _Time.y) - 0.5) + _Time.y) < _Sandstorm)
			{
				col.r = rand(uv + float2(123 + _Time.y, 0));
				col.g = rand(uv + float2(123 + _Time.y, 1));
				col.b = rand(uv + float2(123 + _Time.y, 2));
			}

			// ピクセルごとに描画するRGBを決める
			//float floorX = fmod(inUV.x * _ScreenParams.x / 3, 1);
			//col.r *= floorX > 0.4444;
			//col.g *= floorX < 0.4444 || floorX > 0.7777;
			//col.b *= floorX < 0.7777;

			// スキャンラインを描画
			float scanLineColor = sin(_Time.y * 10 + uv.y * 500) / 2 + 0.5;
			col *= 0.5 + clamp(scanLineColor + 0.5, 0, 1) * 0.5;

			// スキャンラインの残像を描画
			float tail = clamp((frac(uv.y + _Time.y * 100) - 1 + 2) / min(2, 1), 0, 1);
			col *= tail;

			// 画面端を暗くする
			col *= 1 - vignet * _Brightness;


			return float4(col, _Alpha);
		}//frag
			ENDCG
		}//pass


		}//SubShader
}
