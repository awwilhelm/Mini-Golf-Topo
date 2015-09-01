Shader "Custom/HeightDashedLines" {
	Properties {
    _Seperation ("Seperation", Float) = 10
    _LineWidth ("LineWidth", Float) = 1
    _LineColor ("LineColor", Color) = (0, 0, 0)
    _BackgroundColor ("BackgroundColor", Color) = (0, 0, 0)
    _Percision ("Percision", int) = 10000
    [HideInInspector] _WorldPosRemainder ("WorldPosRemainder", Float) = 100
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
    
    float _Seperation;
    float _LineWidth;
    float4 _LineColor;
    float4 _BackgroundColor;
    float _Percision;
    float _WorldPosRemainder;
    
    void surf (Input IN, inout SurfaceOutput o) {
    	o.Albedo = _BackgroundColor;
    	
    	//This equation only does worldPos % _Seperation, but floats don't have the percision to give the correct output
    	_WorldPosRemainder = (IN.worldPos.y - (floor(0.5+ floor(IN.worldPos.y/_Seperation) * _Seperation *_Percision) /_Percision));
    	
    	if (_WorldPosRemainder >= 0 && _WorldPosRemainder <= _LineWidth)
    		o.Albedo = _LineColor;
    }
    ENDCG
}

	FallBack "Diffuse"
}
