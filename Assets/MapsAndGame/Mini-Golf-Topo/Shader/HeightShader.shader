Shader "Custom/HeightShader" {
	Properties {
    _PeakColor ("PeakColor", Color) = (0.8,0.9,0.9,1)   
    _PeakLevel ("PeakLevel", Float) = 300
    _Level8Color ("Level8Color", Color) = (0.75,0.53,0,1)
    _Level8 ("Level8", Float) = 30
    _Level7Color ("Level7Color", Color) = (0.75,0.53,0,1)
    _Level7 ("Level7", Float) = 30
    _Level6Color ("Level6Color", Color) = (0.75,0.53,0,1)
    _Level6 ("Level6", Float) = 30
    _Level5Color ("Level5Color", Color) = (0.75,0.53,0,1)
    _Level5 ("Level5", Float) = 30
    _Level4Color ("Level4Color", Color) = (0.75,0.53,0,1)
    _Level4 ("Level4", Float) = 30
    _Level3Color ("Level3Color", Color) = (0.75,0.53,0,1)
    _Level3 ("Level3", Float) = 20
    _Level2Color ("Level2Color", Color) = (0.69,0.63,0.31,1)
    _Level2 ("Level2", Float) = 10
    _Level1Color ("Level1Color", Color) = (0.65,0.86,0.63,1)
    _Level1 ("Level1", Float) = 5
    _WaterColor ("WaterColor", Color) = (0.37,0.78,0.92,1)
    _WaterLevel ("WaterLevel", Float) = 0
    _Slope ("Slope Fader", Range (0,1)) = 0
}
SubShader {

    Tags { "RenderType" = "Opaque" }
    Fog { Mode Off }
    CGPROGRAM
    #pragma vertex vert
    #pragma surface surf Lambert
    
    //#pragma surface surf Lambert vertex:vert
    struct Input {
        float3 customColor;
        float3 worldPos;
    };
    void vert (inout appdata_full v, out Input o) {
    	UNITY_INITIALIZE_OUTPUT(Input,o);
        o.customColor = abs(v.normal.y);
    }
    float _PeakLevel;
    float4 _PeakColor;
    float _Level8;
    float4 _Level8Color;
    float _Level7;
    float4 _Level7Color;
    float _Level6;
    float4 _Level6Color;
    float _Level5;
    float4 _Level5Color;
    float _Level4;
    float4 _Level4Color;
    float _Level3;
    float4 _Level3Color;
    float _Level2;
    float4 _Level2Color;
    float _Level1;
    float4 _Level1Color;
    float _Slope;
    float _WaterLevel;
    float4 _WaterColor;
    void surf (Input IN, inout SurfaceOutput o) {
    	if (IN.worldPos.y >= _PeakLevel)
    		o.Albedo = _PeakColor;
        else if (IN.worldPos.y >= _Level8)
        	o.Albedo = _Level8Color;
        else if (IN.worldPos.y >= _Level7)
        	o.Albedo = _Level7Color;
        else if (IN.worldPos.y >= _Level6)
        	o.Albedo = _Level6Color;
        else if (IN.worldPos.y >= _Level5)
        	o.Albedo = _Level5Color;
        else if (IN.worldPos.y >= _Level4)
        	o.Albedo = _Level4Color;
        else if (IN.worldPos.y >= _Level3)
        	o.Albedo = _Level3Color;
        else if (IN.worldPos.y >= _Level2)
        	o.Albedo = _Level2Color;
        else if (IN.worldPos.y >= _Level1)
        	o.Albedo = _Level1Color;
        else
        	o.Albedo = _WaterColor; 
    }
    ENDCG
}

	FallBack "Diffuse"
}
