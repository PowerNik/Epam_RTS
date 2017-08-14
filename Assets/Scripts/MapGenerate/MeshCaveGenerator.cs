using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MapGenerate;

public class MeshCaveGenerator : MonoBehaviour
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
	}

	private void TriangulateSquare(Square sq)
	{
		switch (sq.configration)
		{
			case SquareConfiguration.NullPoint:
				break;

			case SquareConfiguration.OnePoint_1:
				MeshFromPoints(sq.centreBot, sq.botLeft, sq.centreLeft);
				break;
			case SquareConfiguration.OnePoint_2:
				MeshFromPoints(sq.centreRight, sq.botRight, sq.centreBot);
				break;
			case SquareConfiguration.OnePoint_4:
				MeshFromPoints(sq.centreTop, sq.topRight, sq.centreRight);
				break;
			case SquareConfiguration.OnePoint_8:
				MeshFromPoints(sq.topLeft, sq.centreTop, sq.centreLeft);
				break;

			case SquareConfiguration.TwoPoint_3:
				MeshFromPoints(sq.centreRight, sq.botRight, sq.botLeft, sq.centreLeft);
				break;
			case SquareConfiguration.TwoPoint_6:
				MeshFromPoints(sq.centreTop, sq.topRight, sq.botRight, sq.centreBot);
				break;
			case SquareConfiguration.TwoPoint_9:
				MeshFromPoints(sq.topLeft, sq.centreTop, sq.centreBot, sq.botLeft);
				break;
			case SquareConfiguration.TwoPoint_12:
				MeshFromPoints(sq.topLeft, sq.topRight, sq.centreRight, sq.centreLeft);
				break;
			case SquareConfiguration.TwoPoint_5:
				MeshFromPoints(sq.centreTop, sq.topRight, sq.centreRight, sq.centreBot, sq.botLeft, sq.centreLeft);
				break;
			case SquareConfiguration.TwoPoint_10:
				MeshFromPoints(sq.topLeft, sq.centreTop, sq.centreRight, sq.botRight, sq.centreBot, sq.centreLeft);
				break;

			case SquareConfiguration.ThreePoint_7:
				MeshFromPoints(sq.centreTop, sq.topRight, sq.botRight, sq.botLeft, sq.centreLeft);
				break;
			case SquareConfiguration.ThreePoint_11:
				MeshFromPoints(sq.topLeft, sq.centreTop, sq.centreRight, sq.botRight, sq.botLeft);
				break;
			case SquareConfiguration.ThreePoint_13:
				MeshFromPoints(sq.topLeft, sq.topRight, sq.centreRight, sq.centreBot, sq.botLeft);
				break;
			case SquareConfiguration.ThreePoint_14:
				MeshFromPoints(sq.topLeft, sq.topRight, sq.botRight, sq.centreBot, sq.centreLeft);
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
