Shader "Custom/ArrowShader" {
	Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        [HideInInspector]_TexWidth ("Texture Size", Float) = 512
        _test1 ("test1", Color) = (1,1,1,1)
        _test2 ("test2", Color) = (0.9, 0.8, 0.7, 1)
        _test3 ("test3", Color) = (0.2, 0.3, 0.4, 1)
        _worldPos("worldPos", Float) = 0.5
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        
	    CGPROGRAM
    	#pragma vertex vert
	    #pragma surface surf Lambert

        sampler2D _MainTex;
        float _TexWidth;
        float4 _test1;
        float4 _test2;
        float4 _test3;
        float _worldPos;

        struct Input {
            float2 uv_MainTex;  
        	float3 worldPos;
        };
        
        void vert (inout appdata_full v, out Input o) {
	    	UNITY_INITIALIZE_OUTPUT(Input,o);
	    }

        void surf (Input IN, inout SurfaceOutput o) {
            //o.Albedo = Color.blue;
            _worldPos=IN.worldPos.x;
            o.Albedo = _test3;
            if(_TexWidth < 20)
            	o.Albedo = _test1;
            else
            	o.Albedo = _test2;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}
