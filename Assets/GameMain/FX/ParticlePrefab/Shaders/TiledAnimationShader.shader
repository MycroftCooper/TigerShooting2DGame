// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.17 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.17;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|emission-4260-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32427,y:33259,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Time,id:565,x:31097,y:33233,varname:node_565,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9579,x:31326,y:33212,varname:node_9579,prsc:2|A-6627-OUT,B-565-T;n:type:ShaderForge.SFN_Relay,id:6627,x:31326,y:33095,varname:node_6627,prsc:2|IN-8147-Z;n:type:ShaderForge.SFN_Vector4Property,id:8147,x:31068,y:32891,ptovrint:False,ptlb:XY Tile,ptin:_XYTile,varname:node_8147,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Frac,id:3833,x:31488,y:33239,varname:node_3833,prsc:2|IN-9579-OUT;n:type:ShaderForge.SFN_Multiply,id:4227,x:31488,y:33101,varname:node_4227,prsc:2|A-1802-OUT,B-643-OUT;n:type:ShaderForge.SFN_Relay,id:1802,x:31385,y:32798,varname:node_1802,prsc:2|IN-8147-X;n:type:ShaderForge.SFN_Relay,id:643,x:31385,y:32932,varname:node_643,prsc:2|IN-8147-Y;n:type:ShaderForge.SFN_Multiply,id:6785,x:31670,y:33178,varname:node_6785,prsc:2|A-4227-OUT,B-3833-OUT;n:type:ShaderForge.SFN_Round,id:3072,x:31832,y:33198,varname:node_3072,prsc:2|IN-6785-OUT;n:type:ShaderForge.SFN_Divide,id:8561,x:32056,y:33198,varname:node_8561,prsc:2|A-3072-OUT,B-1802-OUT;n:type:ShaderForge.SFN_Floor,id:5273,x:32255,y:33208,varname:node_5273,prsc:2|IN-8561-OUT;n:type:ShaderForge.SFN_Divide,id:4637,x:31950,y:33002,varname:node_4637,prsc:2|A-5273-OUT,B-643-OUT;n:type:ShaderForge.SFN_OneMinus,id:9174,x:32150,y:33002,varname:node_9174,prsc:2|IN-4637-OUT;n:type:ShaderForge.SFN_Append,id:3196,x:32168,y:32821,varname:node_3196,prsc:2|A-5808-OUT,B-9174-OUT;n:type:ShaderForge.SFN_Divide,id:5808,x:31950,y:32720,varname:node_5808,prsc:2|A-9249-OUT,B-1802-OUT;n:type:ShaderForge.SFN_Fmod,id:9249,x:31950,y:32868,varname:node_9249,prsc:2|A-3072-OUT,B-1802-OUT;n:type:ShaderForge.SFN_Add,id:5826,x:32344,y:32831,varname:node_5826,prsc:2|A-7163-OUT,B-3196-OUT;n:type:ShaderForge.SFN_Divide,id:7163,x:32344,y:32680,varname:node_7163,prsc:2|A-405-UVOUT,B-1134-OUT;n:type:ShaderForge.SFN_TexCoord,id:405,x:32344,y:32500,varname:node_405,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:1134,x:32127,y:32625,varname:node_1134,prsc:2|A-1802-OUT,B-643-OUT;n:type:ShaderForge.SFN_Tex2d,id:823,x:32384,y:33030,ptovrint:False,ptlb:Tex Sheet,ptin:_TexSheet,varname:node_823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5826-OUT;n:type:ShaderForge.SFN_Multiply,id:4260,x:32560,y:33106,varname:node_4260,prsc:2|A-823-RGB,B-1304-RGB;proporder:1304-823-8147;pass:END;sub:END;*/

Shader "Shader Forge/TiledAnimationShader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _TexSheet ("Tex Sheet", 2D) = "white" {}
        _XYTile ("XY Tile", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float4 _XYTile;
            uniform sampler2D _TexSheet; uniform float4 _TexSheet_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float node_1802 = _XYTile.r;
                float node_643 = _XYTile.g;
                float4 node_565 = _Time + _TimeEditor;
                float node_3072 = round(((node_1802*node_643)*frac((_XYTile.b*node_565.g))));
                float2 node_5826 = ((i.uv0/float2(node_1802,node_643))+float2((fmod(node_3072,node_1802)/node_1802),(1.0 - (floor((node_3072/node_1802))/node_643))));
                float4 _TexSheet_var = tex2D(_TexSheet,TRANSFORM_TEX(node_5826, _TexSheet));
                float3 emissive = (_TexSheet_var.rgb*_Color.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
