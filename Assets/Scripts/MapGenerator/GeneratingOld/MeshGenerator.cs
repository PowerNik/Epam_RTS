using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MapGenerate;

public class MeshGenerator : MonoBehaviour
{
	public SquareGrid squareGrid;

	private List<Vector3> vertices;
	private List<int> triangles;

	public void GenerateMesh(int[,] map, float squareSize)
	{
		squareGrid = new SquareGrid(map, squareSize);

		vertices = new List<Vector3>();
		triangles = new List<int>();

		int nodeCountX = squareGrid.squares.GetLength(0);
		int nodeCountY = squareGrid.squares.GetLength(1);

		for (int x = 0; x < nodeCountX; x++)
		{
			for (int y = 0; y < nodeCountY; y++)
			{
				TriangulateSquare(squareGrid.squares[x, y]);
			}
		}

		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	private void TriangulateSquare(Square sq)
	{
		switch (sq.configration)
		{
			case SquareConfiguration.NullPoint:
				break;

			case SquareConfiguration.OnePoint_1:
				MeshFromPoints(sq.centreBot, sq.botLeft, sq.centreLeft);
				MeshFromPoints(sq.centreBot, sq.centreLeft, sq.wallCentreLeft, sq.wallCentreBot);
				break;
			case SquareConfiguration.OnePoint_2:
				MeshFromPoints(sq.centreRight, sq.botRight, sq.centreBot);
				MeshFromPoints(sq.centreRight, sq.centreBot, sq.wallCentreBot, sq.wallCentreRight);
				break;
			case SquareConfiguration.OnePoint_4:
				MeshFromPoints(sq.centreTop, sq.topRight, sq.centreRight);
				MeshFromPoints(sq.centreTop, sq.centreRight, sq.wallCentreRight, sq.wallCentreTop);
				break;
			case SquareConfiguration.OnePoint_8:
				MeshFromPoints(sq.topLeft, sq.centreTop, sq.centreLeft); 
				MeshFromPoints(sq.centreLeft, sq.centreTop, sq.wallCentreTop, sq.wallCentreLeft);
				break;

			case SquareConfiguration.TwoPoint_3:
				MeshFromPoints(sq.centreRight, sq.botRight, sq.botLeft, sq.centreLeft);
				MeshFromPoints(sq.centreRight, sq.centreLeft, sq.wallCentreLeft, sq.wallCentreRight);
				break;
			case SquareConfiguration.TwoPoint_6:
				MeshFromPoints(sq.centreTop, sq.topRight, sq.botRight, sq.centreBot);
				MeshFromPoints(sq.centreTop, sq.centreBot, sq.wallCentreBot, sq.wallCentreTop);
				break;
			case SquareConfiguration.TwoPoint_9:
				MeshFromPoints(sq.topLeft, sq.centreTop, sq.centreBot, sq.botLeft);
				MeshFromPoints(sq.centreBot, sq.centreTop, sq.wallCentreTop, sq.wallCentreBot);
				break;
			case SquareConfiguration.TwoPoint_12:
				MeshFromPoints(sq.topLeft, sq.topRight, sq.centreRight, sq.centreLeft);
				MeshFromPoints(sq.centreLeft, sq.centreRight, sq.wallCentreRight, sq.wallCentreLeft);
				break;
			case SquareConfiguration.TwoPoint_5: // Имеет 2 стены
				MeshFromPoints(sq.centreTop, sq.topRight, sq.centreRight, sq.centreBot, sq.botLeft, sq.centreLeft);
				MeshFromPoints(sq.centreTop, sq.centreLeft, sq.wallCentreLeft, sq.wallCentreTop);
				MeshFromPoints(sq.centreBot, sq.centreRight, sq.wallCentreRight, sq.wallCentreBot);
				break;
			case SquareConfiguration.TwoPoint_10: // Имеет 2 стены
				MeshFromPoints(sq.topLeft, sq.centreTop, sq.centreRight, sq.botRight, sq.centreBot, sq.centreLeft);
				MeshFromPoints(sq.centreRight, sq.centreTop, sq.wallCentreTop, sq.wallCentreRight);
				MeshFromPoints(sq.centreLeft, sq.centreBot, sq.wallCentreBot, sq.wallCentreLeft);
				break;

			case SquareConfiguration.ThreePoint_7:
				MeshFromPoints(sq.centreTop, sq.topRight, sq.botRight, sq.botLeft, sq.centreLeft);
				MeshFromPoints(sq.centreTop, sq.centreLeft, sq.wallCentreLeft, sq.wallCentreTop);
				break;
			case SquareConfiguration.ThreePoint_11:
				MeshFromPoints(sq.topLeft, sq.centreTop, sq.centreRight, sq.botRight, sq.botLeft);
				MeshFromPoints(sq.centreRight, sq.centreTop, sq.wallCentreTop, sq.wallCentreRight);
				break;
			case SquareConfiguration.ThreePoint_13:
				MeshFromPoints(sq.topLeft, sq.topRight, sq.centreRight, sq.centreBot, sq.botLeft);
				MeshFromPoints(sq.centreBot, sq.centreRight, sq.wallCentreRight, sq.wallCentreBot);
				break;
			case SquareConfiguration.ThreePoint_14:
				MeshFromPoints(sq.topLeft, sq.topRight, sq.botRight, sq.centreBot, sq.centreLeft);
				MeshFromPoints(sq.centreLeft, sq.centreBot, sq.wallCentreBot, sq.wallCentreLeft);
				break;

			case SquareConfiguration.FourPoint_15:
				MeshFromPoints(sq.topLeft, sq.topRight, sq.botRight, sq.botLeft);
				break;
		}
	}

	private void MeshFromPoints(params Node[] points)
	{
		AssignVertices(points);

		// Последовательно создаются треугольники, выходящие из точки начала массива.
		// Чем больше длина массива, тем больше вершин, не лежащих на прямой,
		// тем больше треугольников нужно создать.

		if(points.Length >= 3)
		{
			CreateTriangle(points[0], points[1], points[2]);
		}

		if (points.Length >= 4)
		{
			CreateTriangle(points[0], points[2], points[3]);
		}

		if (points.Length >= 5)
		{
			CreateTriangle(points[0], points[3], points[4]);
		}

		if (points.Length >= 6)
		{
			CreateTriangle(points[0], points[4], points[5]);
		}
	}

	private void AssignVertices(Node[] points)
	{
		for(int i = 0; i < points.Length; i++)
		{
			if(points[i].vertexIndex == -1)
			{
				points[i].vertexIndex = vertices.Count;
				vertices.Add(points[i].position);
			}
		}

	}

	private void CreateTriangle(Node a, Node b, Node c)
	{
		triangles.Add(a.vertexIndex);
		triangles.Add(b.vertexIndex);
		triangles.Add(c.vertexIndex);
	}
}
