// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:Particles/Additive,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4795,x:32825,y:32683,varname:node_4795,prsc:2|emission-1075-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32414,y:33151,ptovrint:False,ptlb:AnimTex,ptin:_AnimTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2982-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32464,y:32696,varname:node_2393,prsc:2|A-2053-RGB,B-797-RGB,C-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32226,y:32434,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32129,y:32590,ptovrint:True,ptlb:MainColor,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32095,y:32752,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector4Property,id:3131,x:31708,y:33211,ptovrint:False,ptlb:AixsSpeed,ptin:_AixsSpeed,varname:node_3131,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_TexCoord,id:5467,x:31870,y:32880,varname:node_5467,prsc:2,uv:0;n:type:ShaderForge.SFN_Color,id:1101,x:32427,y:32892,ptovrint:False,ptlb:FlowColor,ptin:_FlowColor,varname:node_1101,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Time,id:5317,x:31708,y:33046,varname:node_5317,prsc:2;n:type:ShaderForge.SFN_Add,id:7911,x:32121,y:32890,varname:node_7911,prsc:2|A-5467-U,B-6287-OUT;n:type:ShaderForge.SFN_Add,id:4443,x:32145,y:33075,varname:node_4443,prsc:2|A-5467-V,B-6220-OUT;n:type:ShaderForge.SFN_Multiply,id:6287,x:31938,y:33065,varname:node_6287,prsc:2|A-5317-T,B-3131-X;n:type:ShaderForge.SFN_Multiply,id:6220,x:31949,y:33228,varname:node_6220,prsc:2|A-5317-T,B-3131-Y;n:type:ShaderForge.SFN_Append,id:2982,x:32262,y:32959,varname:node_2982,prsc:2|A-7911-OUT,B-4443-OUT;n:type:ShaderForge.SFN_Multiply,id:3917,x:32551,y:33035,varname:node_3917,prsc:2|A-1101-RGB,B-6074-RGB;n:type:ShaderForge.SFN_Add,id:1075,x:32631,y:32824,varname:node_1075,prsc:2|A-2393-OUT,B-3917-OUT;proporder:797-1101-3131-6074;pass:END;sub:END;*/

Shader "TLG/ShieldShader" {
    Properties {
        _TintColor ("MainColor", Color) = (0.5,0.5,0.5,1)
        _FlowColor ("FlowColor", Color) = (0.5,0.5,0.5,1)
        _AixsSpeed ("AixsSpeed", Vector) = (0,0,0,0)
        _AnimTex ("AnimTex", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform sampler2D _AnimTex; uniform float4 _AnimTex_ST;
            uniform float4 _TintColor;
            uniform float4 _AixsSpeed;
            uniform float4 _FlowColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_5317 = _Time + _TimeEditor;
                float2 node_2982 = float2((i.uv0.r+(node_5317.g*_AixsSpeed.r)),(i.uv0.g+(node_5317.g*_AixsSpeed.g)));
                float4 _AnimTex_var = tex2D(_AnimTex,TRANSFORM_TEX(node_2982, _AnimTex));
                float3 emissive = ((i.vertexColor.rgb*_TintColor.rgb*2.0)+(_FlowColor.rgb*_AnimTex_var.rgb));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Particles/Additive"
    CustomEditor "ShaderForgeMaterialInspector"
}
