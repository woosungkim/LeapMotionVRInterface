using UnityEngine;


public class MeshBuilder {
	
	public Mesh Mesh { get; private set; }
	
	public Vector3[] Vertices { get; private set; }
	public Vector2[] Uvs { get; private set; }
	public Color32[] Colors { get; private set; }
	public int[] Triangles { get; private set; }
	
	public int VertexIndex { get; private set; }
	public int UvIndex { get; private set; }
	public int TriangleIndex { get; private set; }

	/*************************************************************************/
	public MeshBuilder(Mesh mesh=null) {
		Mesh = (mesh ?? new Mesh());
		Mesh.MarkDynamic();
		
		if ( mesh != null ) {
			Vertices = mesh.vertices;
			Uvs = mesh.uv;
			Colors = mesh.colors32;
			Triangles = mesh.triangles;
			
			if ( Colors.Length != Vertices.Length ) {
				Colors = new Color32[Vertices.Length];
				CommitColors(Color.white);
			}
		}
	}
	
	
	/*************************************************************************/
	public void Resize(int vertexCount, int triangleCount) {
		if ( Vertices == null || vertexCount != Vertices.Length ) {
			Vertices = new Vector3[vertexCount];
			Uvs = new Vector2[vertexCount];
			Colors = new Color32[vertexCount];
			Mesh.Clear();
		}
		
		if ( Triangles == null || triangleCount != Triangles.Length ) {
			Triangles = new int[triangleCount];
			Mesh.Clear();
		}
	}
	
	/*--------------------------------------------------------------------------------------------*/
	public void ResetIndices() {
		VertexIndex = 0;
		UvIndex = 0;
		TriangleIndex = 0;
	}
	
	

	/*************************************************************************/
	public void AddVertex(Vector3 vertex) {
		Vertices[VertexIndex++] = vertex;
	}
	
	/*************************************************************************/
	public void AddUv(Vector2 uv) {
		Uvs[UvIndex++] = uv;
	}
	
	/*************************************************************************/
	public void AddRemainingUvs(Vector2 uv) {
		while ( UvIndex < Uvs.Length ) {
			Uvs[UvIndex++] = uv;
		}
	}
	
	/*************************************************************************/
	public void AddTriangle(int a, int b, int c) {
		Triangles[TriangleIndex++] = a;
		Triangles[TriangleIndex++] = b;
		Triangles[TriangleIndex++] = c;
	}
	
	
	/*************************************************************************/
	public void Commit(bool recalcNormals=false, bool optimize=false) {
		Mesh.vertices = Vertices;
		Mesh.uv = Uvs;
		Mesh.triangles = Triangles;
		
		Mesh.RecalculateBounds();
		
		if ( recalcNormals ) {
			Mesh.RecalculateNormals();
		}
		
		if ( optimize ) {
			Mesh.Optimize();
		}
	}

	/*************************************************************************/
	

	public void CommitColors(Color32 color) {
		for ( int i = 0 ; i < Colors.Length ; i++ ) {
			Colors[i] = color;
		}
		
		Mesh.colors32 = Colors;
	}
	
}
