/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:True,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.4852941,fgcg:0.4852941,fgcb:0.4852941,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32718,y:32700|diff-69-OUT,spec-90-OUT,normal-3-RGB,emission-28-OUT,amspl-95-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33485,y:32324,ptlb:diff,ptin:_diff,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3,x:33169,y:32722,ptlb:nm,ptin:_nm,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Fresnel,id:23,x:33356,y:32932|EXP-24-OUT;n:type:ShaderForge.SFN_Multiply,id:24,x:33587,y:32932|A-25-OUT,B-27-OUT;n:type:ShaderForge.SFN_Slider,id:25,x:33825,y:32927,ptlb:Fresnel,ptin:_Fresnel,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:27,x:33825,y:33000,v1:10;n:type:ShaderForge.SFN_Multiply,id:28,x:33101,y:32978|A-2-A,B-23-OUT;n:type:ShaderForge.SFN_Multiply,id:30,x:33210,y:33244|A-2-A,B-31-RGB,C-31-A,D-76-OUT;n:type:ShaderForge.SFN_Cubemap,id:31,x:33505,y:33289,ptlb:Cubemap,ptin:_Cubemap,cube:f466cf7415226e046b096197eb7341aa,pvfc:0;n:type:ShaderForge.SFN_Vector1,id:32,x:33502,y:33610,v1:10;n:type:ShaderForge.SFN_Multiply,id:69,x:33004,y:32327|A-2-RGB,B-70-RGB;n:type:ShaderForge.SFN_Color,id:70,x:33258,y:32158,ptlb:Main Color,ptin:_MainColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:76,x:33343,y:33508|A-80-OUT,B-32-OUT;n:type:ShaderForge.SFN_Slider,id:80,x:33550,y:33512,ptlb:Reflection Power,ptin:_ReflectionPower,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Color,id:88,x:33550,y:32554,ptlb:Specular Color,ptin:_SpecularColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:90,x:33181,y:32451|A-2-A,B-88-RGB;n:type:ShaderForge.SFN_Color,id:93,x:33166,y:33469,ptlb:Reflection Color,ptin:_ReflectionColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:95,x:32995,y:33206|A-30-OUT,B-93-RGB;proporder:70-88-25-93-80-2-3-31;pass:END;sub:END;*/

Shader "Reflection Custom/Reflection Specular" {
    Properties {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _Fresnel ("Fresnel", Range(0, 1)) = 1
        _ReflectionColor ("Reflection Color", Color) = (1,1,1,1)
        _ReflectionPower ("Reflection Power", Range(0, 1)) = 1
        _diff ("Main(RGB) Specular(A)", 2D) = "white" {}
        _nm ("Normal", 2D) = "bump" {}
        _Cubemap ("Cubemap", Cube) = "_Skybox" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _diff; uniform float4 _diff_ST;
            uniform sampler2D _nm; uniform float4 _nm_ST;
            uniform float _Fresnel;
            uniform samplerCUBE _Cubemap;
            uniform float4 _MainColor;
            uniform float _ReflectionPower;
            uniform float4 _SpecularColor;
            uniform float4 _ReflectionColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_561 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_nm,TRANSFORM_TEX(node_561.rg, _nm))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb*2;
////// Emissive:
                float4 node_2 = tex2D(_diff,TRANSFORM_TEX(node_561.rg, _diff));
                float node_28 = (node_2.a*pow(1.0-max(0,dot(normalDirection, viewDirection)),(_Fresnel*10.0)));
                float3 emissive = float3(node_28,node_28,node_28);
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_31 = texCUBE(_Cubemap,viewReflectDirection);
                float3 specularColor = (node_2.a*_SpecularColor.rgb);
                float3 specularAmb = ((node_2.a*node_31.rgb*node_31.a*(_ReflectionPower*10.0))*_ReflectionColor.rgb) * specularColor;
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor + specularAmb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (node_2.rgb*_MainColor.rgb);
                finalColor += specular;
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _diff; uniform float4 _diff_ST;
            uniform sampler2D _nm; uniform float4 _nm_ST;
            uniform float _Fresnel;
            uniform float4 _MainColor;
            uniform float4 _SpecularColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_562 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_nm,TRANSFORM_TEX(node_562.rg, _nm))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_2 = tex2D(_diff,TRANSFORM_TEX(node_562.rg, _diff));
                float3 specularColor = (node_2.a*_SpecularColor.rgb);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (node_2.rgb*_MainColor.rgb);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "Custom"
}
