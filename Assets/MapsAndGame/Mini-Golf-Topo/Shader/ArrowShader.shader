Shader "Custom/ArrowShader" {
	Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        [HideInInspector]_TexWidth ("Texture Size", float) = 50
        
        _lowest10 ("Lowest 10", Color) = (1,1,1,1)
        _lowest20 ("Lowest 20", Color) = (0.9, 0.8, 0.7, 1)
        _lowest30 ("Lowest 30", Color) = (0.2, 0.3, 0.4, 1)
        _lowest40 ("Lowest 40", Color) = (1,1,1,1)
        _lowest50 ("Lowest 50", Color) = (0.9, 0.8, 0.7, 1)
        _lowest60 ("Lowest 60", Color) = (0.2, 0.3, 0.4, 1)
        _lowest70 ("Lowest 70", Color) = (1,1,1,1)
        _lowest80 ("Lowest 80", Color) = (0.9, 0.8, 0.7, 1)
        _lowest90 ("Lowest 90", Color) = (0.2, 0.3, 0.4, 1)
        _lowest100 ("Lowest 100", Color) = (0.2, 0.3, 0.4, 1)
        _blendVal ("Blend Value", Range(0,1)) = 1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        
	    CGPROGRAM
    	#pragma vertex vert
	    #pragma surface surf Lambert
		#pragma target 3.0

        sampler2D _MainTex;
        float _TexWidth;
        float _TexMaxWidth;
        half3 _lowest10;
        half3 _lowest20;
        half3 _lowest30;
        half3 _lowest40;
        half3 _lowest50;
        half3 _lowest60;
        half3 _lowest70;
        half3 _lowest80;
        half3 _lowest90;
        half3 _lowest100;
        float _blendVal;

        struct Input {
            float2 uv_MainTex;  
        	float3 worldPos;
        };
        
        void vert (inout appdata_full v, out Input o) {
	    	UNITY_INITIALIZE_OUTPUT(Input,o);
	    }

        void surf (Input IN, inout SurfaceOutput o) {
            if(_TexWidth < 0.10)
            	o.Albedo = _lowest10;  
            else if(_TexWidth < 0.20)
            	o.Albedo = lerp(_lowest10, _lowest20, _blendVal);
            else if(_TexWidth < 0.30)
            	o.Albedo = lerp(_lowest20, _lowest30, _blendVal);
            else if(_TexWidth < 0.40)
            	o.Albedo = lerp(_lowest30, _lowest40, _blendVal);
            else if(_TexWidth < 0.50)
            	o.Albedo = lerp(_lowest40, _lowest50, _blendVal);
            else if(_TexWidth < 0.60)
            	o.Albedo = lerp(_lowest50, _lowest60, _blendVal);
           	else if(_TexWidth < 0.70)
            	o.Albedo = lerp(_lowest60, _lowest70, _blendVal);
            else if(_TexWidth < 0.80)
            	o.Albedo = lerp(_lowest70, _lowest80, _blendVal);
            else if(_TexWidth < 0.90)
            	o.Albedo = lerp(_lowest80, _lowest90, _blendVal);
            else
            	o.Albedo = lerp(_lowest90, _lowest100, _blendVal);
        }
        ENDCG
    } 
    FallBack "Diffuse"
}
