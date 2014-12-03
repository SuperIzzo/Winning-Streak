#ifndef LuxIBL_CG_INCLUDED
#define LuxIBL_CG_INCLUDED

// Lux IBL / ambient lighting
		// set o.Emission = 0.0 to make diffuse shaders work correctly
		o.Emission = 0.0;

		#ifdef NORMAL_IS_WORLDNORMAL
			float3 worldNormal = IN.normal;
		#else
			float3 worldNormal = WorldNormalVector (IN, o.Normal);
		#endif

		#if defined(USE_GLOBAL_DIFFIBL_SETTINGS) && defined(GLDIFFCUBE_ON)
			#define DIFFCUBE_ON
		#endif

//		add diffuse IBL
		#ifdef DIFFCUBE_ON
			fixed4	diff_ibl = texCUBE (_DiffCubeIBL, worldNormal);
			#ifdef LUX_LINEAR
				// if colorspace = linear alpha has to be brought to linear too (rgb already is): alpha = pow(alpha,2.233333333).
				// approximation taken from http://chilliant.blogspot.de/2012/08/srgb-approximations-for-hlsl.html
				diff_ibl.a *= diff_ibl.a * (diff_ibl.a * 0.305306011 + 0.682171111) + 0.012522878;
			#endif
			diff_ibl.rgb = diff_ibl.rgb * diff_ibl.a;
			o.Emission = diff_ibl.rgb * ExposureIBL.x * o.Albedo;
		#else
			#if defined (LIGHTMAP_OFF) && defined (DIRLIGHTMAP_OFF)
//			otherwise add ambient light from Spherical Harmonics
				o.Emission = ShadeSH9 ( float4(worldNormal.xyz, 1.0)) * o.Albedo;
			#endif
		#endif
		
//		add specular IBL		
		#ifdef SPECCUBE_ON
			#ifdef LUX_BOXPROJECTION
				half3 worldRefl;
			#else
				half3 worldRefl = WorldReflectionVector (IN, o.Normal);	
			#endif
			
		//	Boxprojection
			// Using OBB volume
			#ifdef LUX_BOXPROJECTION

				float3 DirectionWS = normalize(IN.worldPos - _WorldSpaceCameraPos);
				float3 ReflDirectionWS = reflect(DirectionWS, worldNormal);

				float3 RayLS = mul( _World2Object, float4(ReflDirectionWS, 0.0f));
				float3 PositionLS = mul( _World2Object, float4(IN.worldPos, 1.0f));

				float3 FirstPlaneIntersect  = (_CubemapSize - PositionLS) / RayLS;
				float3 SecondPlaneIntersect = (-_CubemapSize - PositionLS) / RayLS;
				float3 FurthestPlane = max(FirstPlaneIntersect, SecondPlaneIntersect);
				
				float Distance = min(FurthestPlane.x, min(FurthestPlane.y, FurthestPlane.z));
				float3 IntersectPosWS = IN.worldPos + ReflDirectionWS * Distance;
				worldRefl = IntersectPosWS - _CubemapPositionWS;
			#endif

			#if defined (LUX_LIGHTING_CT)
				o.Specular *= o.Specular * (o.Specular * 0.305306011 + 0.682171111) + 0.012522878;
			#endif
			float mipSelect = 1.0f - o.Specular;
			mipSelect = mipSelect * mipSelect * 7; // but * 6 would look better...
			fixed4 spec_ibl = texCUBElod (_SpecCubeIBL, float4(worldRefl, mipSelect));
			
			#ifdef LUX_LINEAR
				// if colorspace = linear alpha has to be brought to linear too (rgb already is): alpha = pow(alpha,2.233333333).
				// approximation taken from http://chilliant.blogspot.de/2012/08/srgb-approximations-for-hlsl.html
				spec_ibl.a *= spec_ibl.a * (spec_ibl.a * 0.305306011 + 0.682171111) + 0.012522878;
			#endif
			spec_ibl.rgb = spec_ibl.rgb * spec_ibl.a;
			// fresnel based on spec_albedo.rgb and roughness (spec_albedo.a)
			// taken from: http://seblagarde.wordpress.com/2011/08/17/hello-world/
			// viewDir is in tangent-space (as we sample o.Normal) so we use o.Normal
			//			
			float3 FresnelSchlickWithRoughness = o.SpecularColor + ( max(o.Specular, o.SpecularColor ) - o.SpecularColor) * exp2(-OneOnLN2_x6 * saturate(dot(normalize(IN.viewDir), o.Normal)));	
	//		colorize fresnel highlights and make it look like marmoset:
	//		float3 FresnelSchlickWithRoughness = o.SpecularColor + o.Specular.xxx * o.SpecularColor * exp2(-OneOnLN2_x6 * saturate(dot(normalize(IN.viewDir), o.Normal)));		
				
			spec_ibl.rgb *= FresnelSchlickWithRoughness * ExposureIBL.y;
			// add diffuse and specular and conserve energy
			o.Emission = (1 - spec_ibl.rgb) * o.Emission + spec_ibl.rgb;
		#endif
		
		#ifdef LUX_AO_ON
			half ambientOcclusion = tex2D(_AO, IN.uv_AO).a;
			o.Emission *= ambientOcclusion;
		#endif

		#ifdef LUX_METALNESS
			o.Emission *= spec_albedo.g;
		#endif
		
#endif
