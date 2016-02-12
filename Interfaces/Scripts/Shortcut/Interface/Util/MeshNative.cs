using UnityEngine;
using System.Collections;
using System;

public static class MeshNative {



	public static Vector3 GetArcPoint(float radius, float angle) {
		float x = (float)Math.Sin(angle);
		float y = (float)Math.Cos(angle);
		return new Vector3(x*radius, 0, y*radius);
	}

	public static void BuildArcMesh(MeshBuilder meshBuilder,
	                               float innerRadius,
	                               float outerRadius,
	                               float startAngle,
	                               float endAngle,
	                               int steps) {

		float angleFull = endAngle - startAngle;
		float angleInc = angleFull / steps;
		float angle = startAngle;

		meshBuilder.Resize((steps+1)*2, steps*6);
		meshBuilder.ResetIndices ();

		for (int i = 0; i <= steps; ++i) {
			float uv = i/(float)steps;

			meshBuilder.AddVertex(GetArcPoint(innerRadius, angle));
			meshBuilder.AddVertex(GetArcPoint(outerRadius, angle));

			meshBuilder.AddUv(new Vector2(uv, 0));
			meshBuilder.AddUv(new Vector2(uv, 1));

			if ( i > 0 ) {
				int vi = meshBuilder.VertexIndex;
				meshBuilder.AddTriangle(vi-3, vi-4, vi-2);
				meshBuilder.AddTriangle(vi-1, vi-3, vi-2);
			}

			angle += angleInc;
		}
	}

	public static void BuildRectangleMesh(MeshBuilder meshBuilder, 
	                                      float width,
	                                      float height, 
	                                      float wStart,
	                                      float hStart,
	                                      float amount) {
		float fullW;
		float fullH;
		
		if ( width >= height ) {
			fullH = height*amount;
			fullW = width-(height-fullH);
		}
		else {
			fullW = width*amount;
			fullH = height-(width-fullW);
		}

		meshBuilder.Resize(4, 6);
		meshBuilder.ResetIndices();
		
		meshBuilder.AddVertex(new Vector3(fullW,  fullH, 0));
		meshBuilder.AddVertex(new Vector3(fullW, fullH*hStart, 0)); 
		meshBuilder.AddVertex(new Vector3(fullW*wStart, fullH*hStart, 0));
		meshBuilder.AddVertex(new Vector3(fullW*wStart, fullH, 0));
		
		meshBuilder.AddTriangle(0, 1, 2);
		meshBuilder.AddTriangle(0, 2, 3);
		
		meshBuilder.AddRemainingUvs(Vector2.zero);
	}


}
