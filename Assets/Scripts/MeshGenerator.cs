using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
	public SquareGrid squareGrid;

	void OnDrawGizmos()
	{
		if (squareGrid != null)
		{
			int nodeCountX = squareGrid.squares.GetLength(0);
			int nodeCountY = squareGrid.squares.GetLength(1);

			Vector3 mainCubeSize = Vector3.one * 0.4f;
			Vector3 otherCubeSize = Vector3.one * 0.25f;

			for (int x = 0; x < nodeCountX; x++)
			{
				for (int y = 0; y < nodeCountY; y++)
				{
					Gizmos.color = (squareGrid.squares[x, y].topLeft.active) ? Color.black : Color.white;
					Gizmos.DrawCube(squareGrid.squares[x, y].topLeft.position, mainCubeSize);

					Gizmos.color = (squareGrid.squares[x, y].topRight.active) ? Color.black : Color.white;
					Gizmos.DrawCube(squareGrid.squares[x, y].topRight.position, mainCubeSize);

					Gizmos.color = (squareGrid.squares[x, y].bottomRight.active) ? Color.black : Color.white;
					Gizmos.DrawCube(squareGrid.squares[x, y].bottomRight.position, mainCubeSize);

					Gizmos.color = (squareGrid.squares[x, y].bottomLeft.active) ? Color.black : Color.white;
					Gizmos.DrawCube(squareGrid.squares[x, y].bottomLeft.position, mainCubeSize);

					Gizmos.color = Color.yellow;
					Gizmos.DrawCube(squareGrid.squares[x, y].centreTop.position, otherCubeSize);
					Gizmos.DrawCube(squareGrid.squares[x, y].centreRight.position, otherCubeSize);
					Gizmos.DrawCube(squareGrid.squares[x, y].centreBottom.position, otherCubeSize);
					Gizmos.DrawCube(squareGrid.squares[x, y].centreLeft.position, otherCubeSize);
				}
			}
		}
	}

	public void GenerateMesh(int[,] map, float squareSize)
	{
		squareGrid = new SquareGrid(map, squareSize);
	}
}

public class SquareGrid
{
	public Square[,] squares;

	public SquareGrid(int[,] map, float squareSize)
	{
		int nodeCountX = map.GetLength(0);
		int nodeCountY = map.GetLength(1);
		float mapWidth = nodeCountX * squareSize;
		float mapHeight = nodeCountY * squareSize;

		ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

		for (int x = 0; x < nodeCountX; x++)
		{
			for (int y = 0; y < nodeCountY; y++)
			{
				float curX = -mapWidth / 2f + x * squareSize + squareSize / 2f;
				float curZ = -mapHeight / 2f + y * squareSize + squareSize / 2f;

				Vector3 pos = new Vector3(curX, 0, curZ);
				controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1, squareSize);
			}
		}

		squares = new Square[nodeCountX - 1, nodeCountY - 1];
		for (int x = 0; x < nodeCountX - 1; x++)
		{
			for (int y = 0; y < nodeCountY - 1; y++)
			{
				ControlNode bottomLeft = controlNodes[x, y];
				ControlNode topLeft = controlNodes[x, y + 1];
				ControlNode topRight = controlNodes[x + 1, y + 1];
				ControlNode bottomRight = controlNodes[x + 1, y];

				squares[x, y] = new Square(topLeft, topRight, bottomRight, bottomLeft);
			}
		}
	}
}

public class Square
{
	// Вершины квадрата
	public ControlNode topLeft, topRight, bottomRight, bottomLeft;

	// Середины сторон квадрата
	public Node centreTop, centreBottom, centreRight, centreLeft;

	public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomRight, ControlNode bottomLeft)
	{
		// Обход вершин по часовой стрелке
		this.topLeft = topLeft;
		this.topRight = topRight;
		this.bottomRight = bottomRight;
		this.bottomLeft = bottomLeft;

		// TL--CT--TR
		// |        |
		// CL------CR
		// |        |
		// BL--CB--BR

		centreTop = topLeft.right;
		centreBottom = bottomLeft.right;
		centreRight = bottomRight.above;
		centreLeft = bottomLeft.above;
	}
}


public class Node
{
	public Vector3 position;
	public int vertexIndex = -1;

	public Node(Vector3 position)
	{
		this.position = position;
	}
}

public class ControlNode : Node
{
	public bool active;
	public Node above, right;

	public ControlNode(Vector3 position, bool active, float squareSize) : base(position)
	{
		this.active = active;
		above = new Node(position + Vector3.forward * squareSize / 2f);
		right = new Node(position + Vector3.right * squareSize / 2f);
	}
}
