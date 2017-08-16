using UnityEngine;

namespace MapGenerate
{
	public class SquareGrid
	{
		public Square[,] squares;
		public float noiseScale = 2.5f;
		public float noiseHeight = 4f;

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

					float curY = noiseHeight * Mathf.PerlinNoise(x / noiseScale, y / noiseScale);

					Vector3 pos = new Vector3(curX, curY, curZ);
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
}
