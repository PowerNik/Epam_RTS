using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
	int width;
	int height;
	float tileSize;

	public void GenerateMesh(int[,] map, float squareSize)
	{
		width = map.GetLength(0);
		height = map.GetLength(1);
		tileSize = squareSize;

		BuildMesh();
		//BuildTexture();
	}

	private void BuildTexture()
	{
		Texture2D texture = new Texture2D(width, height);

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();

		//GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
	}

	private void BuildMesh()
	{
		int vertCountX = width + 1;
		int vertCountZ = height + 1;

		int vertCount = vertCountX * vertCountZ;
		int tileCount = width * height;
		int trisCount = tileCount * 2 * 3; // Тайл состоит из двух треугольников, у каждого 3 вершины

		Vector3[] vertices = new Vector3[vertCount];
		Vector3[] normals = new Vector3[vertCount];
		Vector2[] uv = new Vector2[vertCount];

		int[] triangles = new int[trisCount];

		for (int z = 0; z < vertCountZ; z++)
		{
			for (int x = 0; x < vertCountX; x++)
			{
				int squareIndex = z * vertCountX + x;

				vertices[squareIndex] = new Vector3(-width / 2 + x * tileSize, 0, -height / 2 + z * tileSize);
				normals[squareIndex] = Vector3.up;
				uv[squareIndex] = new Vector2((float)x / width, (float)z / height);
			}
		}

		for (int z = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{
				int squareIndex = z * width + x;
				int triOffset = squareIndex * 6;

				triangles[triOffset + 0] = z * vertCountX + x +              0;
				triangles[triOffset + 1] = z * vertCountX + x + vertCountX + 0;
				triangles[triOffset + 2] = z * vertCountX + x + vertCountX + 1;

				triangles[triOffset + 3] = z * vertCountX + x +              0;
				triangles[triOffset + 4] = z * vertCountX + x + vertCountX + 1;
				triangles[triOffset + 5] = z * vertCountX + x +              1;
			}
		}

		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uv;
		mesh.triangles = triangles;

		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}
}
