// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2148-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31755,y:32692,ptovrint:False,ptlb:MainTexA,ptin:_MainTexA,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:1,isnm:False|UVIN-4610-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32009,y:32692,varname:node_2393,prsc:2|A-797-RGB,B-6074-RGB;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32054,y:33107,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:31755,y:32520,ptovrint:True,ptlb:ColorA,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:8765,x:31795,y:33177,ptovrint:False,ptlb:MsinTexB,ptin:_MsinTexB,varname:node_8765,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4852941,c2:0.4852941,c3:0.4852941,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7793,x:31795,y:32966,ptovrint:False,ptlb:ColorB,ptin:_ColorB,varname:node_7793,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:1,isnm:False|UVIN-7859-OUT;n:type:ShaderForge.SFN_TexCoord,id:1700,x:31129,y:32644,varname:node_1700,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:2525,x:31130,y:33387,varname:node_2525,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:3144,x:30885,y:32861,varname:node_3144,prsc:2;n:type:ShaderForge.SFN_Multiply,id:978,x:32009,y:32910,varname:node_978,prsc:2|A-7793-RGB,B-8765-RGB;n:type:ShaderForge.SFN_Multiply,id:2148,x:32344,y:32906,varname:node_2148,prsc:2|A-9991-OUT,B-2053-RGB,C-6133-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6133,x:32043,y:33297,ptovrint:False,ptlb:Colorful,ptin:_Colorful,varname:node_6133,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Vector4Property,id:9210,x:30885,y:33078,ptovrint:False,ptlb:MoveSpeed,ptin:_MoveSpeed,varname:node_9210,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Multiply,id:477,x:31129,y:32820,varname:node_477,prsc:2|A-3144-T,B-9210-X;n:type:ShaderForge.SFN_Add,id:3692,x:31333,y:32727,varname:node_3692,prsc:2|A-1700-U,B-477-OUT;n:type:ShaderForge.SFN_Multiply,id:4227,x:31129,y:32963,varname:node_4227,prsc:2|A-3144-T,B-9210-Y;n:type:ShaderForge.SFN_Add,id:3841,x:31333,y:32865,varname:node_3841,prsc:2|A-1700-V,B-4227-OUT;n:type:ShaderForge.SFN_Append,id:4610,x:31534,y:32812,varname:node_4610,prsc:2|A-3692-OUT,B-3841-OUT;n:type:ShaderForge.SFN_Multiply,id:2185,x:31130,y:33110,varname:node_2185,prsc:2|A-3144-T,B-9210-Z;n:type:ShaderForge.SFN_Add,id:6985,x:31335,y:33145,varname:node_6985,prsc:2|A-2185-OUT,B-2525-U;n:type:ShaderForge.SFN_Multiply,id:6479,x:31130,y:33240,varname:node_6479,prsc:2|A-3144-T,B-9210-W;n:type:ShaderForge.SFN_Add,id:4386,x:31335,y:33377,varname:node_4386,prsc:2|A-6479-OUT,B-2525-V;n:type:ShaderForge.SFN_Append,id:7859,x:31514,y:33306,varname:node_7859,prsc:2|A-6985-OUT,B-4386-OUT;n:type:ShaderForge.SFN_Add,id:9991,x:32210,y:32761,varname:node_9991,prsc:2|A-2393-OUT,B-978-OUT;proporder:797-6074-8765-7793-6133-9210;pass:END;sub:END;*/

Shader "TLG/UVAnimTowTexsVertexColor" {
    Properties {
        _TintColor ("ColorA", Color) = (0.5,0.5,0.5,1)
        _MainTexA ("MainTexA", 2D) = "gray" {}
        _MsinTexB ("MsinTexB", Color) = (0.4852941,0.4852941,0.4852941,1)
        _ColorB ("ColorB", 2D) = "gray" {}
        _Colorful ("Colorful", Float ) = 2
        _MoveSpeed ("MoveSpeed", Vector) = (0,0,0,0)
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
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles metal 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTexA; uniform float4 _MainTexA_ST;
            uniform float4 _TintColor;
            uniform float4 _MsinTexB;
            uniform sampler2D _ColorB; uniform float4 _ColorB_ST;
            uniform float _Colorful;
            uniform float4 _MoveSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_3144 = _Time + _TimeEditor;
                float2 node_4610 = float2((i.uv0.r+(node_3144.g*_MoveSpeed.r)),(i.uv0.g+(node_3144.g*_MoveSpeed.g)));
                float4 _MainTexA_var = tex2D(_MainTexA,TRANSFORM_TEX(node_4610, _MainTexA));
                float2 node_7859 = float2(((node_3144.g*_MoveSpeed.b)+i.uv0.r),((node_3144.g*_MoveSpeed.a)+i.uv0.g));
                float4 _ColorB_var = tex2D(_ColorB,TRANSFORM_TEX(node_7859, _ColorB));
                float3 emissive = (((_TintColor.rgb*_MainTexA_var.rgb)+(_ColorB_var.rgb*_MsinTexB.rgb))*i.vertexColor.rgb*_Colorful);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
